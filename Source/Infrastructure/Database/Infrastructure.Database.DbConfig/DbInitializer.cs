using Administration.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Database.DbConfig
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            AddCountries(context);
            AddCities(context);
        }

        private static void AddCountries(ApplicationDbContext context)
        {
            if (context.Countries.Count() > 1)
            {
                return;
            }

            context.Countries.AddRange(GetCoutryList());
            context.SaveChanges();
        }

        private static void AddCities(ApplicationDbContext context)
        {
            if (context.Cities.Count() > 1)
            {
                return;
            }

            context.Cities.AddRange(GetCityList());
            context.SaveChanges();
        }

        private static IEnumerable<Country> GetCoutryList()
        {
            var result = new List<Country>();
            result.Add(new Country() { Name = "Полша", NameEn = "Poland", Code = "PL", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Гърция", NameEn = "Greece", Code = "GR", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Турция", NameEn = "Turkey", Code = "TR", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Украйна", NameEn = "Ukraine", Code = "UA", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Германия", NameEn = "Germany", Code = "DE", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Франция", NameEn = "France", Code = "FR", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Австрия", NameEn = "Austria", Code = "AT", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Испания", NameEn = "Spain", Code = "ES", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "България", NameEn = "Bulgaria", Code = "BG", Version = 1, CreatedOn = DateTime.Now, Highlight = true });
            result.Add(new Country() { Name = "Англия", NameEn = "United Kingdom", Code = "GB", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Китай", NameEn = "China", Code = "CN", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Тайван", NameEn = "Taiwan, Province of China", Code = "TW", Version = 1, CreatedOn = DateTime.Now });

            result.Add(new Country() { Name = "Австралия", NameEn = "Australia", Code = "AU", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Азербайджан", NameEn = "Azerbaijan", Code = "AZ", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Аландски острови", NameEn = "Åland Islands", Code = "AX", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Албания", NameEn = "Albania", Code = "AL", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Алжир", NameEn = "Algeria", Code = "DZ", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Американска Самоа", NameEn = "American Samoa", Code = "AS", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Американски Вирджински острови", NameEn = "Virgin Islands, U.S.", Code = "VI", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Ангола", NameEn = "Angola", Code = "AO", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Ангуила", NameEn = "Anguilla", Code = "AI", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Андора", NameEn = "Andorra", Code = "AD", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Антарктика", NameEn = "Antarctica", Code = "AQ", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Антигуа и Барбуда", NameEn = "Antigua and Barbuda", Code = "AG", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Аржентина", NameEn = "Argentina", Code = "AR", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Армения", NameEn = "Armenia", Code = "AM", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Аруба", NameEn = "Aruba", Code = "AW", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Афганистан", NameEn = "Afghanistan", Code = "AF", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Бангладеш", NameEn = "Bangladesh", Code = "BD", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Барбадос", NameEn = "Barbados", Code = "BB", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Бахамски острови", NameEn = "Bahamas", Code = "BS", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Бахрейн", NameEn = "Bahrain", Code = "BH", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Беларус", NameEn = "Belarus", Code = "BY", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Белгия", NameEn = "Belgium", Code = "BE", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Белиз", NameEn = "Belize", Code = "BZ", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Бенин", NameEn = "Benin", Code = "BJ", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Бермудски острови", NameEn = "Bermuda", Code = "BM", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Боливия", NameEn = "Bolivia", Code = "BO", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Босна и Херцеговина", NameEn = "Bosnia and Herzegovina", Code = "BA", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Ботсвана", NameEn = "Botswana", Code = "BW", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Бразилия", NameEn = "Brazil", Code = "BR", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Британска територия в Индийския океан", NameEn = "British Indian Ocean Territory", Code = "IO", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Британски Вирджински острови", NameEn = "Virgin Islands, British", Code = "VG", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Бруней Даруссалам", NameEn = "Brunei Darussalam", Code = "BN", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Бряг на слоновата кост (Кот д'Ивоар),", NameEn = "Côte d'Ivoire", Code = "CI", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Буве", NameEn = "Bouvet Island", Code = "BV", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Буркина Фасо", NameEn = "Burkina Faso", Code = "BF", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Бурунди", NameEn = "Burundi", Code = "BI", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Бутан", NameEn = "Bhutan", Code = "BT", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Вануату", NameEn = "Vanuatu", Code = "VU", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Ватикана", NameEn = "Holy See (Vatican City State),", Code = "VA", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Венецуела", NameEn = "Venezuela, Bolivarian Republic of", Code = "VE", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Виетнам", NameEn = "Viet Nam", Code = "VN", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Габон", NameEn = "Gabon", Code = "GA", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Гамбия", NameEn = "Gambia", Code = "GM", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Гана", NameEn = "Ghana", Code = "GH", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Гаяна", NameEn = "Guyana", Code = "GY", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Гваделупа", NameEn = "Guadeloupe", Code = "GP", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Гватемала", NameEn = "Guatemala", Code = "GT", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Гвинея", NameEn = "Guinea", Code = "GN", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Гвинея-Бисау", NameEn = "Guinea-Bissau", Code = "GW", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Гибралтар", NameEn = "Gibraltar", Code = "GI", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Гренада", NameEn = "Grenada", Code = "GD", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Гренландия", NameEn = "Greenland", Code = "GL", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Грузия", NameEn = "Georgia", Code = "GE", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Гуам", NameEn = "Guam", Code = "GU", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Гърнси", NameEn = "Guernsey", Code = "GG", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Дания", NameEn = "Denmark", Code = "DK", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Демократична република Конго (Заир),", NameEn = "Congo, the Democratic Republic of the", Code = "CD", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Джибути", NameEn = "Djibouti", Code = "DJ", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Джърси", NameEn = "Jersey", Code = "JE", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Доминика", NameEn = "Dominica", Code = "DM", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Доминиканска република", NameEn = "Dominican Republic", Code = "DO", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Египет", NameEn = "Egypt", Code = "EG", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Еквадор", NameEn = "Ecuador", Code = "EC", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Екваториална Гвинея", NameEn = "Equatorial Guinea", Code = "GQ", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Ел Салвадор", NameEn = "El Salvador", Code = "SV", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Еритрея", NameEn = "Eritrea", Code = "ER", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Естония", NameEn = "Estonia", Code = "EE", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Етиопия", NameEn = "Ethiopia", Code = "ET", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Замбия", NameEn = "Zambia", Code = "ZM", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Западна Сахара", NameEn = "Western Sahara", Code = "EH", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Зимбабве", NameEn = "Zimbabwe", Code = "ZW", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Йемен", NameEn = "Yemen", Code = "YE", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Израел", NameEn = "Israel", Code = "IL", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Източен Тимор", NameEn = "Timor-Leste", Code = "TL", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Индия", NameEn = "India", Code = "IN", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Индонезия", NameEn = "Indonesia", Code = "ID", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Йордания", NameEn = "Jordan", Code = "JO", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Ирак", NameEn = "Iraq", Code = "IQ", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Иран", NameEn = "Iran, Islamic Republic of", Code = "IR", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Ирландия", NameEn = "Ireland", Code = "IE", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Исландия", NameEn = "Iceland", Code = "IS", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Италия", NameEn = "Italy", Code = "IT", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Кабо Верде (острови Зелени Нос),", NameEn = "Cape Verde", Code = "CV", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Казахстан", NameEn = "Kazakhstan", Code = "KZ", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Каймански острови", NameEn = "Cayman Islands", Code = "KY", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Камбоджа", NameEn = "Cambodia", Code = "KH", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Камерун", NameEn = "Cameroon", Code = "CM", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Канада", NameEn = "Canada", Code = "CA", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Карибска Холандия", NameEn = "Bonaire, Sint Eustatius and Saba", Code = "BQ", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Катар", NameEn = "Qatar", Code = "QA", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Кения", NameEn = "Kenya", Code = "KE", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Кипър", NameEn = "Cyprus", Code = "CY", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Киргизстан", NameEn = "Kyrgyzstan", Code = "KG", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Кирибати", NameEn = "Kiribati", Code = "KI", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Кокосови острови", NameEn = "Cocos (Keeling), Islands", Code = "CC", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Коледни острови", NameEn = "Christmas Island", Code = "CX", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Колумбия", NameEn = "Colombia", Code = "CO", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Коморски острови", NameEn = "Comoros", Code = "KM", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Конго", NameEn = "Congo", Code = "CG", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Коста Рика", NameEn = "Costa Rica", Code = "CR", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Куба", NameEn = "Cuba", Code = "CU", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Кувейт", NameEn = "Kuwait", Code = "KW", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Кюрасао", NameEn = "Curaçao", Code = "CW", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Лаос", NameEn = "Laos", Code = "LA", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Латвия", NameEn = "Latvia", Code = "LV", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Лесото", NameEn = "Lesotho", Code = "LS", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Либерия", NameEn = "Liberia", Code = "LR", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Либия", NameEn = "Libya", Code = "LY", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Ливан", NameEn = "Lebanon", Code = "LB", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Литва", NameEn = "Lithuania", Code = "LT", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Лихтенщайн", NameEn = "Liechtenstein", Code = "LI", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Люксембург", NameEn = "Luxembourg", Code = "LU", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Мавритания", NameEn = "Mauritania", Code = "MR", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Мавриций", NameEn = "Mauritius", Code = "MU", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Мадагаскар", NameEn = "Madagascar", Code = "MG", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Майот", NameEn = "Mayotte", Code = "YT", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Макао", NameEn = "Macao", Code = "MO", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Македония", NameEn = "Macedonia, the Former Yugoslav Republic of", Code = "MK", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Малави", NameEn = "Malawi", Code = "MW", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Малайзия", NameEn = "Malaysia", Code = "MY", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Малдиви", NameEn = "Maldives", Code = "MV", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Мали", NameEn = "Mali", Code = "ML", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Малта", NameEn = "Malta", Code = "MT", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Ман (остров),", NameEn = "Isle of Man", Code = "IM", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Мароко", NameEn = "Morocco", Code = "MA", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Мартиника", NameEn = "Martinique", Code = "MQ", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Маршалови острови", NameEn = "Marshall Islands", Code = "MH", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Мексико", NameEn = "Mexico", Code = "MX", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Мианмар", NameEn = "Myanmar", Code = "MM", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Микронезия", NameEn = "Micronesia, Federated States of", Code = "FM", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Мозамбик", NameEn = "Mozambique", Code = "MZ", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Молдова", NameEn = "Moldova, Republic of", Code = "MD", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Монако", NameEn = "Monaco", Code = "MC", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Монголия", NameEn = "Mongolia", Code = "MN", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Монсерат", NameEn = "Montserrat", Code = "MS", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Намибия", NameEn = "Namibia", Code = "NA", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Науру", NameEn = "Nauru", Code = "NR", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Непал", NameEn = "Nepal", Code = "NP", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Нигер", NameEn = "Niger", Code = "NE", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Нигерия", NameEn = "Nigeria", Code = "NG", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Никарагуа", NameEn = "Nicaragua", Code = "NI", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Ниуе", NameEn = "Niue", Code = "NU", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Нова Зеландия", NameEn = "New Zealand", Code = "NZ", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Нова Каледония", NameEn = "New Caledonia", Code = "NC", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Норвегия", NameEn = "Norway", Code = "NO", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Норфолк (остров),", NameEn = "Norfolk Island", Code = "NF", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Обединени арабски емирства", NameEn = "United Arab Emirates", Code = "AE", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Оман", NameEn = "Oman", Code = "OM", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Острови Кук", NameEn = "Cook Islands", Code = "CK", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Острови Хърд и Макдоналд", NameEn = "Heard Island and McDonald Islands", Code = "HM", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Пакистан", NameEn = "Pakistan", Code = "PK", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Палау", NameEn = "Palau", Code = "PW", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Палестина", NameEn = "Palestine, State of", Code = "PS", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Панама", NameEn = "Panama", Code = "PA", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Папуа Нова Гвинея", NameEn = "Papua New Guinea", Code = "PG", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Парагвай", NameEn = "Paraguay", Code = "PY", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Перу", NameEn = "Peru", Code = "PE", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Питкерн", NameEn = "Pitcairn", Code = "PN", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Португалия", NameEn = "Portugal", Code = "PT", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Пуерто Рико", NameEn = "Puerto Rico", Code = "PR", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Реюнион", NameEn = "Réunion", Code = "RE", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Руанда", NameEn = "Rwanda", Code = "RW", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Румъния", NameEn = "Romania", Code = "RO", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Русия", NameEn = "Russian Federation", Code = "RU", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Самоа", NameEn = "Samoa", Code = "WS", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Сан марино", NameEn = "San Marino", Code = "SM", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Сао Томе и Принсипи", NameEn = "Sao Tome and Principe", Code = "ST", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Саудитска Арабия", NameEn = "Saudi Arabia", Code = "SA", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "САЩ", NameEn = "United States", Code = "US", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Свазиленд", NameEn = "Swaziland", Code = "SZ", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Свалбард и Ян Майен", NameEn = "Svalbard and Jan Mayen", Code = "SJ", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Света Елена (остров),", NameEn = "Saint Helena, Ascension and Tristan da Cunha", Code = "SH", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Северна Корея", NameEn = "Korea, Democratic People's Republic of", Code = "KP", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Северни Мариански острови", NameEn = "Northern Mariana Islands", Code = "MP", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Сейнт Бартс", NameEn = "Saint Barthélemy", Code = "BL", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Сейнт Винсент и Гренадини", NameEn = "Saint Vincent and the Grenadines", Code = "VC", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Сейнт Китс и Невис", NameEn = "Saint Kitts and Nevis", Code = "KN", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Сейнт Лусия", NameEn = "Saint Lucia", Code = "LC", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Сейшели", NameEn = "Seychelles", Code = "SC", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Сен Мартен (Франция),", NameEn = "Saint Martin (French part),", Code = "MF", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Сен Пиер и Микелон", NameEn = "Saint Pierre and Miquelon", Code = "PM", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Сенегал", NameEn = "Senegal", Code = "SN", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Сиера Леоне", NameEn = "Sierra Leone", Code = "SL", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Сингапур", NameEn = "Singapore", Code = "SG", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Синт Мартен (Холандия),", NameEn = "Sint Maarten (Dutch part),", Code = "SX", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Сирия", NameEn = "Syrian Arab Republic", Code = "SY", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Словакия", NameEn = "Slovakia", Code = "SK", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Словения", NameEn = "Slovenia", Code = "SI", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Соломонови острови", NameEn = "Solomon Islands", Code = "SB", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Сомалия", NameEn = "Somalia", Code = "SO", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Судан", NameEn = "Sudan", Code = "SD", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Суринам", NameEn = "Suriname", Code = "SR", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Сърбия", NameEn = "Serbia", Code = "RS", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Таджикистан", NameEn = "Tajikistan", Code = "TJ", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Тайланд", NameEn = "Thailand", Code = "TH", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Танзания", NameEn = "Tanzania, United Republic of", Code = "TZ", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Того", NameEn = "Togo", Code = "TG", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Токелау", NameEn = "Tokelau", Code = "TK", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Тонга", NameEn = "Tonga", Code = "TO", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Тринидад и Тобаго", NameEn = "Trinidad and Tobago", Code = "TT", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Тувалу", NameEn = "Tuvalu", Code = "TV", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Тунис", NameEn = "Tunisia", Code = "TN", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Туркменистан", NameEn = "Turkmenistan", Code = "TM", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Търкс и Кайкос", NameEn = "Turks and Caicos Islands", Code = "TC", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Уганда", NameEn = "Uganda", Code = "UG", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Узбекистан", NameEn = "Uzbekistan", Code = "UZ", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Унгария", NameEn = "Hungary", Code = "HU", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Уолис и Футуна", NameEn = "Wallis and Futuna", Code = "WF", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Уругвай", NameEn = "Uruguay", Code = "UY", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Фарьорски острови", NameEn = "Faroe Islands", Code = "FO", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Фиджи", NameEn = "Fiji", Code = "FJ", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Филипини", NameEn = "Philippines", Code = "PH", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Финландия", NameEn = "Finland", Code = "FI", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Фолкландски острови", NameEn = "Falkland Islands (Malvinas),", Code = "FK", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Френска Полинезия", NameEn = "French Polynesia", Code = "PF", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Френски южни и антарктически територии", NameEn = "French Southern Territories", Code = "TF", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Фреска Гвиана", NameEn = "French Guiana", Code = "GF", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Хаити", NameEn = "Haiti", Code = "HT", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Холандия", NameEn = "Netherlands", Code = "NL", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Хондурас", NameEn = "Honduras", Code = "HN", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Хонконг", NameEn = "Hong Kong", Code = "HK", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Хърватска", NameEn = "Croatia", Code = "HR", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Централноафриканска република", NameEn = "Central African Republic", Code = "CF", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Чад", NameEn = "Chad", Code = "TD", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Черна гора", NameEn = "Montenegro", Code = "ME", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Чехия", NameEn = "Czech Republic", Code = "CZ", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Чили", NameEn = "Chile", Code = "CL", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Швейцария", NameEn = "Switzerland", Code = "CH", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Швеция", NameEn = "Sweden", Code = "SE", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Шри Ланка", NameEn = "Sri Lanka", Code = "LK", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "ЮАР", NameEn = "South Africa", Code = "ZA", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Южен Судан", NameEn = "South Sudan", Code = "SS", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Южна Джорджия и Южни Сандвичеви острови", NameEn = "South Georgia and the South Sandwich Islands", Code = "GS", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Южна Корея", NameEn = "South Korea", Code = "KR", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Ямайка", NameEn = "Jamaica", Code = "JM", Version = 1, CreatedOn = DateTime.Now });
            result.Add(new Country() { Name = "Япония", NameEn = "Japan", Code = "JP", Version = 1, CreatedOn = DateTime.Now });

            return result;
        }

        private static IEnumerable<City> GetCityList()
        {
            var result = new List<City>();
            result.Add(new City() { Name = "Асеновград", Highlight = true, PostCode = 4230, Type = CityTypeEnum.City, Version = 1, CreatedOn = DateTime.Now });
            result.Add(new City() { Name = "Пловдив", Highlight = true, PostCode = 4000, Type = CityTypeEnum.City, Version = 1, CreatedOn = DateTime.Now });
            result.Add(new City() { Name = "София", Highlight = true, PostCode = 1000, Type = CityTypeEnum.City, Version = 1, CreatedOn = DateTime.Now });

            return result;
        }
    }
}
