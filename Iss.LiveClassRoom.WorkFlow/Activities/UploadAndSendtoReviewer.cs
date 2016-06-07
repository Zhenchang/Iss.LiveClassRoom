using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Iss.LiveClassRoom.Core.Services;
using System.Web;
using Iss.LiveClassRoom.ServiceLayer.Services;
using Iss.LiveClassRoom.DataAccessLayer;
using System.IO;
using Iss.LiveClassRoom.Core.Models;

namespace Iss.LiveClassRoom.WorkFlow.Activities
{

    public sealed class UploadAndSendtoReviewer : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<HttpPostedFileBase> videoFile { get; set; }
        public InArgument<string> Title { get; set; }
        //the local path of file
        //public InArgument<string> FilePath { get; set; }
        public InArgument<string> CourseId { get; set; }
        public InArgument<string> InstructorId { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument

            HttpPostedFileBase videoFile = context.GetValue(this.videoFile);
            SystemContext _db = new SystemContext();
            UnitOfWork _uow = new UnitOfWork(_db);
            ICourseService _courseService = new CourseService(_uow);
            IVideoService _videoService= new VideoService(_uow);
            string CourseId = context.GetValue(this.CourseId);
            string InstructorId = context.GetValue(this.InstructorId);
            string Title = context.GetValue(this.Title);

            //add the file to content folder
            //add the video information to database

            var course =_db.Set<Course>().SingleOrDefault(x => x.Id == CourseId);
            var allowedExt = new string[] { ".mp4", ".ogg", ".mpeg" };
            if (allowedExt.Contains(Path.GetExtension(videoFile.FileName)))
            {
                var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(videoFile.FileName);
                // var path = Path.Combine(Server.MapPath("~/Content/videos"), fileName);
                var path = Path.Combine("~/Content/videos", fileName);
                videoFile.SaveAs(path);
                var domainModel = new Video();
                domainModel.Course = course;
                domainModel.Title = Title;
                domainModel.FileName = fileName;
                //set the isAccept of video to false
                domainModel.IsAccept = false;

                _videoService.Add(domainModel, InstructorId);
            }
            
                //System.IO.File.WriteAllBytes(context.GetValue(FilePath), System.IO.File.ReadAllBytes(""));

            //send the
    }
}
