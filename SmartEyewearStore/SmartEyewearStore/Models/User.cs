﻿namespace SmartEyewearStore.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<SurveyAnswer> Surveys { get; set; }
    }
}
