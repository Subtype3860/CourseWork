using BlogDAL.Models;

namespace BlogBLL.Ext;

public class StringExtention
{
    public static Post GetCutPost(int slong, Post modelPost)
    {
        var postArray = modelPost.Body!.ToCharArray();
        var ellipsis = postArray.Length > slong 
            ? $"{(char)46}{(char)46}{(char)46}{(char)46}{(char)46}{(char)46}{(char)46}{(char)46}" 
            : string.Empty;
        if(postArray.Length > slong)
            Array.Resize(ref postArray, slong);
        modelPost.Body = new string(postArray) + ellipsis;
        return modelPost;
    }
}