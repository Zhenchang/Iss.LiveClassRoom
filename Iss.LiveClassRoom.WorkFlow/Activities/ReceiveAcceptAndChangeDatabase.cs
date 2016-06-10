using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Iss.LiveClassRoom.DataAccessLayer;
using Iss.LiveClassRoom.Core.Services;
using Iss.LiveClassRoom.ServiceLayer.Services;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.WorkFlow.Activities
{

    public sealed class ReceiveAcceptAndChangeDatabase : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<bool> IsAccept { get; set; }
        public InArgument<string> VideoId { get; set; }
        public InArgument<string> AdminId { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            bool IsAccept = context.GetValue(this.IsAccept);
            string VideoId = context.GetValue(this.VideoId);
            string AdminId = context.GetValue(this.AdminId);
            SystemContext _db = new SystemContext();
            UnitOfWork _uow = new UnitOfWork(_db);
            IVideoService _videoService = new VideoService(_uow);

            Task.Run(async () =>
            {
                var video = await _videoService.GetById(VideoId);
                video.IsAccept = IsAccept ? 1 : 0;
                video.Course.ToString();
                await _videoService.Update(video, AdminId);
            }).Wait();
            
        }
        
    }
}
