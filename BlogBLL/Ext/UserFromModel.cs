﻿using BlogBLL.ViewModels.Account;
using BlogDAL.Models;

namespace BlogBLL.Ext
{
    public static class UserFromModel
    {
        public static User Convert(this User user, EditViewModel usereditvm)
        {
            // user.Image = usereditvm.Image;
            // user.LastName = usereditvm.LastName;
            // user.MiddleName = usereditvm.MiddleName;
            // user.FirstName = usereditvm.FirstName;
            // user.Email = usereditvm.Email;
            // user.BirthDate = usereditvm.BirthDate;
            // user.UserName = usereditvm.UserName;
            // user.Status = usereditvm.Status;
            // user.About = usereditvm.About;

            return user;
        }
    }
}
