using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;
using PinkCake.Settings;

namespace PinkCake.Service
{
    public class Worker
    {
        private object _Lock = new object();
        private TimeSpan _SearchDuration = TimeSpan.Zero;
        private ISettingsStore _Store;
        private SourceSettings _Defaults;
        private List<DirectoryInfo> _PhotoFolders = new List<DirectoryInfo>();
        private CancellationTokenSource _AllTasksTokenSource;
        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public Worker(ISettingsStore store, SourceSettings defaults)
        {
            _Store = store;
            _Defaults = defaults;
        }

        private void BuildSearchResults()
        {
            var settings = _Store.Load();
            var photoFolders = settings.Sources
                .SelectMany(s => Directory.GetDirectories(s.RootFolder, string.IsNullOrWhiteSpace(s.Filter) ? "*" : s.Filter, SearchOption.AllDirectories))
                .Select(s => new DirectoryInfo(s))
                .ToList();

            lock (_Lock)
            {
                _PhotoFolders = photoFolders;
            }
        }

        private void SearchUpdate(CancellationToken stoppingToken, Action canceller)
        {
            try
            {
                var settings = _Store.Load();
                double refreshBuffer = _Defaults.RefreshBuffer.Value;

                if (settings.RefreshBuffer != null)
                {
                    refreshBuffer = settings.RefreshBuffer.Value;
                }

                while (!stoppingToken.IsCancellationRequested)
                {
                    var delayTask = Task.Delay(_SearchDuration < TimeSpan.FromMinutes(30) ? TimeSpan.FromMinutes(30) : new TimeSpan((int)(_SearchDuration.Ticks * refreshBuffer)), stoppingToken);

                    Task.WaitAll(delayTask);

                    BuildSearchResults();
                }
            }
            catch(Exception ex)
            {
                new CakeLogger().Log(ex, "Folder search failed");
                canceller();
            }
        }

        private Task<bool> Tick()
        {
            try
            {
                var firstUpdate = _SearchDuration == TimeSpan.Zero;
                DateTime searchStart = DateTime.Now;

                var backgroundFormats = new HashSet<string>(_Defaults.PhotoFilter.Split(','));
                var settings = _Store.Load();

                if (!string.IsNullOrEmpty(settings.PhotoFilter))
                {
                    backgroundFormats = new HashSet<string>(settings.PhotoFilter.Split(',')
                        .Select(s =>
                        {
                            if(s.StartsWith("."))
                            {
                                return s;
                            }

                            return "." + s;
                        }));
                }

                var random = new Random();

                List<FileInfo> photos = new List<FileInfo>();
                var i = 0;

                while (photos.Count == 0 && i < 25)
                {
                    var folder = _PhotoFolders[random.Next(_PhotoFolders.Count)];

                    photos = folder.GetFiles("*", SearchOption.TopDirectoryOnly)
                        .Where(f => backgroundFormats.Contains(f.Extension.ToLower()))
                        .ToList();

                    i++;
                }

                var photo = photos[random.Next(photos.Count)];

                SystemParametersInfo(SPI_SETDESKWALLPAPER,
                    0,
                    photo.FullName,
                    SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);

                var duration = DateTime.Now - searchStart;

                if (!firstUpdate)
                {
                    duration = new TimeSpan((duration + _SearchDuration).Ticks / 2);
                }

                lock (_Lock)
                {
                    _SearchDuration = duration;
                }
            }
            catch (Exception ex)
            {
                new CakeLogger().Log(ex, "Background update failed");
                
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public async Task StartAsync(CancellationToken callingToken)
        {
            lock (_Lock)
            {
                if(_AllTasksTokenSource != null)
                {
                    return;
                }

                _AllTasksTokenSource = CancellationTokenSource.CreateLinkedTokenSource(callingToken);
            }

            var stoppingToken = _AllTasksTokenSource.Token;

            new CakeLogger().Log("Worker running at: " + DateTimeOffset.Now);

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var settings = _Store.Load();
                    var interval = settings.Frequency;

                    BuildSearchResults();

                    _ = Task.Run(() => SearchUpdate(stoppingToken, _AllTasksTokenSource.Cancel));

                    while (!stoppingToken.IsCancellationRequested)
                    {
                        Task delayTask = Task.Delay(interval, stoppingToken);

                        await Tick();
                        await delayTask;
                    }
                }
            }
            catch (Exception ex)
            {
                new CakeLogger().Log(ex, "Main service thread failed");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            lock (_AllTasksTokenSource)
            {
                if(_AllTasksTokenSource == null)
                {
                    return Task.CompletedTask;
                }

                _AllTasksTokenSource.Cancel();

                return Task.CompletedTask;
            }   
        }
    }
}
