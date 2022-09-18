using FloraEducationAPI.Domain.DTO;
using FloraEducationAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Service.Interfaces
{
    public interface ICommentService
    {
        Comment AddCommentToPlant(CommentDTO commentDTO);
    }
}
