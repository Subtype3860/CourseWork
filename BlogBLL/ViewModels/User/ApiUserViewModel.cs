using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogBLL.ViewModels.User
{
    public class ApiUserViewModel
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public string? Id { get; set; }
        /// <summary>
        /// О пользователе
        /// </summary>
        public string About { get; set; } = string.Empty;
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Электронный адрес
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string LastName { get; set; } = string.Empty;
        /// <summary>
        /// Отчество пользователя
        /// </summary>
        public string MiddleName { get; set; } = string.Empty;
        /// <summary>
        /// Телефон
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;
        /// <summary>
        /// Статус
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Логин для входа
        /// </summary>
        public string UserName { get; set; } = string.Empty;
    }
}
