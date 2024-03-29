﻿namespace TasksApp.DataAccess.Entities
{
    public class ProjectEntity
    {
        public string Id { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;
        public string NoteText { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool IsArchived { get; set; } = false;
        public string ColorHex { get; set; } = "FFFFFF"; 
    }
}
