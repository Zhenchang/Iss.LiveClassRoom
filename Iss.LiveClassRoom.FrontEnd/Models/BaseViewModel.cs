using Iss.LiveClassRoom.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iss.LiveClassRoom.FrontEnd.Models
{
    public class BaseViewModel
    {
        [Required]
        public string Id { get; set; }

        public IEntity Entity { get; protected set; }

        public BaseViewModel(IEntity entity)
        {
            Entity = entity;
        }

    }
}