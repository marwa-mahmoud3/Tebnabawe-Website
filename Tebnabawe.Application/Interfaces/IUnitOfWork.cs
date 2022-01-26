using System;
using Tebnabawe.Application.AboutT;
using Tebnabawe.Application.AudioT;
using Tebnabawe.Application.BrochuresT;
using Tebnabawe.Application.GoalsT;
using Tebnabawe.Application.IslamicBooksT;
using Tebnabawe.Application.NewsT;
using Tebnabawe.Application.PhotosLibraryT;
using Tebnabawe.Application.VideoT;
using Tebnabawe.Application.WorksT;

namespace Tebnabawe.Bases.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        #region Methode
        int Commit();
        #endregion
        AboutRepository About { get; }
        GoalsRepository Goals { get; }
        WorkRepository Work { get; }
        NewsRepository News { get; }
        BrochuresRepository Brochures { get; }
        IslamicBookRepository IslamicBooks { get; }
        PhotosLibraryRepository PhotosLibrary { get; }
        AudiosChannelRepository AudiosChannel { get; }
        AudiosLibraryRepository AudiosLibrary { get; }
        VideosChannelRepository VideosChannel { get; }
        VideosLibraryRepository VideosLibrary { get; }
        RadioDataRepositiory RadioData { get; }
        RequestRadioRepository RequestRadio { get; }
    }
}
