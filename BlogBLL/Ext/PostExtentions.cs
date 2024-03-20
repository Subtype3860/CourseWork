using BlogDAL.Repository;
using BlogBLL.UnitOfWork;
using BlogBLL.ViewModels.Post;
using BlogDAL.Models;

namespace BlogBLL.Ext;

public class PostExtentions
{
    private readonly IUnitOfWork _unitOfWork;

    public PostExtentions(IUnitOfWork? unitOfWork)
    {
        _unitOfWork = unitOfWork!;
    }

    #region UpdatePostExt

        /// <summary>
        /// Обновление поста
        /// </summary>
        /// <param name="model"></param>
        public void UpdatePostExt(UpdatePostViewModel model)
        {
            var repository = _unitOfWork.GetRepository<Post>() as PostRepository;
            var post = repository!.GetPostById(model.PostId!);
            post.Body = model.Post;
            repository.PostUpdate(post);
        }

    #endregion

    #region UpdatePostTagExt

        /// <summary>
        /// Обновление связи PostTag
        /// </summary>
        /// <param name="model">UpdatePostViewModel</param>
        public void UpdatePostTagExt(UpdatePostViewModel model)
        {
            var repository = _unitOfWork.GetRepository<PostTag>() as PostTagRepository;
            var postTag = repository!.GetPostTagByPostId(model.PostId!);
            var tagPost = new List<string>();
            //Редактирование PostTag
            foreach (var tag in postTag)
            {
                tagPost.Add(tag.TagId!);
                if (!model.TagId!.Contains(tag.TagId!))
                {
                    repository.DeletePostTag(new PostTag{PostId = model.PostId, TagId = tag.TagId});
                }
            }
            if (model.TagId != null)
            {
                foreach (var tag in model.TagId!)
                {
                    if (!tagPost.Contains(tag))
                    {
                        repository.AddPostTag(new PostTag { PostId = model.PostId, TagId = tag });
                    }
                }
            }
    
        }

    #endregion

    #region GetAllPostExt
    public List<GetAllPostWebApi> GetAllPostExt(IEnumerable<Post> model)
    {
        var getAllPostWebApi = new List<GetAllPostWebApi>();
        foreach (var post in model)
        {
            getAllPostWebApi.Add(new GetAllPostWebApi
            {
                Id = post.Id,
                Heading = post.Heading,
                Body = post.Body,
                DatePublic = post.DatePublic,
                UserId = post.UserId
            });
        }

        return getAllPostWebApi;
    }

    #endregion

}