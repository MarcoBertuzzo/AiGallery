using AiGallery.Components;
using AiGallery.Services;
using Microsoft.EntityFrameworkCore;

namespace AiGallery.Data
{
    public static class DbManager
    {
        /// <summary>
        /// Gets a list of Strips starting from a given id
        /// </summary>
        /// <param name="idFrom">id of the starting Strip</param>
        /// <param name="count">Number of Strips to return</param>
        /// <param name="myDbContext">dbContext</param>
        /// <returns>List of required Strips</returns>
        public async static Task<List<Strip>> getStripRange(int idFrom, int count, MyDbContext myDbContext)
        {
            var stripsInRange = await myDbContext.Strips
                .Where(s => (myDbContext.Strips.Count()-s.Id) >= idFrom && (myDbContext.Strips.Count() - s.Id) <= idFrom + count)
                .OrderByDescending(s => s.Id)
                .Select(s => new Strip
                {
                    ImageStripId = s.Id,
                    Title_Eng = s.Title_Eng,
                    Title_Ita = s.Title_Ita,
                    ViewsCounter = s.ViewsCounter,  
                    Path = "/ImagesStrips/" + s.Id.ToString("000000") + "/1.jpg" //cover
                })
                .ToListAsync();
            return stripsInRange;
        }

        /// <summary>
        /// Get the id of the Strip preceding or following the Strip of a given id
        /// </summary>
        /// <param name="currentImageStripId">Id of the starting Strip</param>
        /// <param name="findNext">findNext=false to get Previous Strip. findNext=true to get Next Strip</param>
        /// <param name="myDbContext">DbContext</param>
        /// <returns>The id of the previous or next Strip</returns>
        public static int GetPreviousOrNextStripId(int currentImageStripId, bool findNext, MyDbContext myDbContext)
        {            
            IQueryable<int> query;

            if (findNext)
            {
                query = myDbContext.Strips
                    .Where(s => s.Id > currentImageStripId)
                    .OrderBy(s => s.Id)
                    .Select(s => s.Id);
            }
            else
            {
                query = myDbContext.Strips
                    .Where(s => s.Id < currentImageStripId)
                    .OrderByDescending(s => s.Id)
                    .Select(s => s.Id);
            }

            var result = query.FirstOrDefault();
            return result;
        }

        /// <summary>
        /// Increment the views counter for a given Strip
        /// </summary>
        /// <param name="imageStripId">Id of the reference Strip</param>
        /// <param name="myDbContext">DbContext</param>
        public static void increaseViewsCounter(int imageStripId, MyDbContext myDbContext)
        {
            var strip = myDbContext.Strips.Find(imageStripId);
            if (strip != null)
            {
                strip.ViewsCounter++;
                strip.LastView = DateTime.Now;
                myDbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the list of Images belonging to a given Strip
        /// </summary>
        /// <param name="stripId">Id of the Strip</param>
        /// <param name="myDbContext">DbContext</param>
        /// <returns>Requested list of images</returns>
        public static async Task<List<Image>> getStripImages(int stripId,  MyDbContext myDbContext)
        {
            var stripImages = await myDbContext.Images
                .Where(s => s.StripId == stripId)
                .OrderBy(s => s.Id)
                .Select(s => new Image
                {
                    StripId = s.StripId,
                    Id = s.Id,
                    RelativePath = "/ImagesStrips/" + s.StripId.ToString("000000") + "/" + s.Id.ToString() + ".jpg",
                    Description_Eng = s.Description_Eng,
                    Description_Ita = s.Description_Ita,
                })
                .ToListAsync();
            return stripImages;
        }


        /// <summary>
        /// Get the title of a Strip in a given language
        /// </summary>
        /// <param name="stripId">Id of the reference Strip</param>
        /// <param name="myDbContext">DbContext</param>
        /// <param name="userLanguage">Language</param>
        /// <returns>The requested Strip title</returns>
        public static string getStripTitle(int stripId, MyDbContext myDbContext, UserLanguage userLanguage)
        {
            return myDbContext.Strips
                .Where(s => s.Id == stripId)
                .Select(s => (userLanguage== UserLanguage.Italian?s.Title_Ita: s.Title_Eng)).First();            
        }

        /// <summary>
        /// Adds a user who has made an image upload request to the database
        /// </summary>
        /// <param name="name">User name</param>
        /// <param name="email">User email</param>
        /// <param name="note">Notes left by the user</param>
        /// <param name="myDbContext">DbContext</param>
        /// <returns>The id of the new user created</returns>
        public static int addUser(string name, string email, string note, MyDbContext myDbContext)
        {
            DbEntities.User user = new DbEntities.User { Name = name, Email = email, Note = note };
            myDbContext.Users.Add(user);
            var count = myDbContext.SaveChangesAsync();
            return user.Id;
        }

        /// <summary>
        /// Adds a reference to an image uploaded by the user to the database
        /// </summary>
        /// <param name="userId">Id of the user who uploaded</param>
        /// <param name="fileName">Name of the saved image file</param>
        /// <param name="myDbContext">DbContext</param>
        /// <returns>The id of the newly uploaded image</returns>
        public static int addUserImage(int userId, string fileName, MyDbContext myDbContext)
        {
            DbEntities.UserImage userImage = new DbEntities.UserImage { UserId = userId, FileName = fileName };
            myDbContext.UserImages.Add(userImage);
            var count = myDbContext.SaveChangesAsync();
            return userImage.Id;
        }

    }
}