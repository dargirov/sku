﻿namespace Administration.Presenters.Dtos
{
    public class FooterActionsViewModel
    {
        public string Controller { get; set; }

        public string Action { get; set; }

        public string DeleteAction { get; set; }

        public string LocalizedBack { get; set; }

        public string LocalizedReset { get; set; }

        public string LocalizedEdit { get; set; }

        public string LocalizedDelete { get; set; }

        public int Id { get; set; }

        public int RouteParam { get; set; }

        public bool ShowReset { get; set; }

        public bool ShowDelete { get; set; }
    }
}
