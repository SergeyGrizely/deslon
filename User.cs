using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deslon
{
    //Класс хранит информацию о пользователях
    class User
    {
        /// <summary>
        /// Возвращает или задает ФИО пользователя
        /// </summary>
        public string first_Name { get; set; }

        /// <summary>
        /// Возвращает или задает ФИО пользователя
        /// </summary>
        public string last_Name { get; set; }

        /// <summary>
        /// Возвращает или задет Логин пользоваетля
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Возвращает или задет пароль пользоваетля
        /// </summary>
        public string Password{ get; set; }


        /// <summary>
        /// Возвращает или задет номер тел пользоваетля
        /// </summary>
        public string Phone{ get; set; }
        public string Id { get; set; }

        public string myId;

        public string im;

        public string FR;




    }
}
