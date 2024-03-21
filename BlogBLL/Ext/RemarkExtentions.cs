using BlogBLL.ViewModels.Comment;
using BlogDAL.Models;

namespace BlogBLL.Ext;

public class RemarkExtentions
{
    public List<ApiGetCommentViewModel> GetRemarkToViewModel(IEnumerable<Remark> remarks)
    {
        var model = new List<ApiGetCommentViewModel>();
        foreach (var remark in remarks)
        {
            model.Add(new ApiGetCommentViewModel
            {
                Id = remark.Id,
                Comment = remark.Comment,
                PostId = remark.PostId,
                UserId = remark.UserId,
                DatePublic = remark.DatePublic
            });
        }

        return model;
    }
}