using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tebnabawe.Application.AboutT;
using Tebnabawe.Application.AudioT;
using Tebnabawe.Application.BrochuresT;
using Tebnabawe.Application.GoalsT;
using Tebnabawe.Application.IslamicBooksT;
using Tebnabawe.Application.NewsT;
using Tebnabawe.Application.PhotosLibraryT;
using Tebnabawe.Application.VideoT;
using Tebnabawe.Application.WorksT;
using Tebnabawe.Bases.Interfaces;
using Tebnabawe.Data;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Application.Bases
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Common Properties
        private DbContext DbContext { get; set; }
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;


        #endregion


        #region Constructors
        public UnitOfWork(TebnabaweContext DbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this.DbContext = DbContext;
        }
        #endregion

        public AboutRepository about;
        public AboutRepository About
        {
            get
            {
                if (about == null)
                    about = new AboutRepository(DbContext);
                return about;
            }
        }
        public GoalsRepository goals;
        public GoalsRepository Goals
        {
            get
            {
                if (goals == null)
                    goals = new GoalsRepository(DbContext);
                return goals;
            }
        }
        public WorkRepository work;
        public WorkRepository Work
        {
            get
            {
                if (work == null)
                    work = new WorkRepository(DbContext);
                return work;
            }
        }
        public NewsRepository news;
        public NewsRepository News
        {
            get
            {
                if (news == null)
                    news = new NewsRepository(DbContext);
                return news;
            }
        }
        public BrochuresRepository brochures;
        public BrochuresRepository Brochures
        {
            get
            {
                if (brochures == null)
                    brochures = new BrochuresRepository(DbContext);
                return brochures;
            }
        }
        public IslamicBookRepository islamicBook;
        public IslamicBookRepository IslamicBooks
        {
            get
            {
                if (islamicBook == null)
                    islamicBook = new IslamicBookRepository(DbContext);
                return islamicBook;
            }
        }
        public PhotosLibraryRepository photosLibrary;
        public PhotosLibraryRepository PhotosLibrary
        {
            get
            {
                if (photosLibrary == null)
                    photosLibrary = new PhotosLibraryRepository(DbContext);
                return photosLibrary;
            }
        }

        public AudiosChannelRepository audiosChannel;
        public AudiosChannelRepository AudiosChannel
        {
            get
            {
                if (audiosChannel == null)
                    audiosChannel = new AudiosChannelRepository(DbContext);
                return audiosChannel;
            }
        }
        public AudiosLibraryRepository audiosLibrary;
        public AudiosLibraryRepository AudiosLibrary
        {
            get
            {
                if (audiosLibrary == null)
                    audiosLibrary = new AudiosLibraryRepository(DbContext);
                return audiosLibrary;
            }
        }
        public VideosChannelRepository videosChannel;
        public VideosChannelRepository VideosChannel
        {
            get
            {
                if (videosChannel == null)
                    videosChannel = new VideosChannelRepository(DbContext);
                return videosChannel;
            }
        }
        public VideosLibraryRepository videosLibrary;
        public VideosLibraryRepository VideosLibrary
        {
            get
            {
                if (videosLibrary == null)
                    videosLibrary = new VideosLibraryRepository(DbContext);
                return videosLibrary;
            }
        }
        public RadioDataRepositiory radioData;
        public RadioDataRepositiory RadioData
        {
            get
            {
                if (radioData == null)
                    radioData = new RadioDataRepositiory(DbContext);
                return radioData;
            }
        }
        public RequestRadioRepository requestRadio;
        public RequestRadioRepository RequestRadio
        {
            get
            {
                if (requestRadio == null)
                    requestRadio = new RequestRadioRepository(DbContext);
                return requestRadio;
            }
        }
        #region Methods
        public int Commit()
        {
            return DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
        #endregion
    }
}
