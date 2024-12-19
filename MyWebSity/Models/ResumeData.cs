namespace MyWebSity.Models
{
    public class ResumeData
    {
        public string FullName { get; set; }
        public string Profession { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AboutMe { get; set; }
        public List<string> Skills { get; set; }
        public List<Education> Education { get; set; }
        public List<Experience> Experience { get; set; }
    }

    public class Education
    {
        public string Institution { get; set; }
        public string Degree { get; set; }
        public string Year { get; set; }
    }

    public class Experience
    {
        public string Company { get; set; }
        public string Position { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
    }
}
