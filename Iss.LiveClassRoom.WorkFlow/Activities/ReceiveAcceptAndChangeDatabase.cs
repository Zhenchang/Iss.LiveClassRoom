using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace Iss.LiveClassRoom.WorkFlow.Activities
{

    public sealed class ReceiveAcceptAndChangeDatabase : NativeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> Text { get; set; }

        protected override void Execute(NativeActivityContext context)
        {
            throw new NotImplementedException();
        }

        
    }
}
