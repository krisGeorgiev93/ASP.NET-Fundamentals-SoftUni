﻿namespace Homies.Models
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Start { get; set; } = null!;

        public string End { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string Organiser { get; init; } = null!;

        public string Description { get; set; } = null!;

        public string CreatedOn { get; set; } = null!;

    }
}
