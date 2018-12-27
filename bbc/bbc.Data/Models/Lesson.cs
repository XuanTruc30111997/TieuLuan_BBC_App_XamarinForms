using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace bbc.Data.Models
{
    public class Lesson
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string IdTP { get; set; }
        public string FileURLOnline { get; set; }
        public string FileURLDowload { get; set; }
        public string ImageURL { get; set; }
        public string Transcript { get; set; }
        public string Actor { get; set; }
        public string Sumary { get; set; }
        public string Vocabulary { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string ImageDownload { get; set; }
       
        //public Lesson(string Id,string Name,int Year,string IdTP,string AudioUrlOnline,string AudioUrlDowload,
        //    string ImageURL,string Transcript,string Actor,string Sumary,string Vocabulary,string CreatedDate,string UpdatedDate)
        //{
        //    this.Id = Id;
        //    this.Name = Name;
        //    this.Year = Year;
        //    this.IdTP = IdTP;
        //    this.AudioUrlOnline = AudioUrlOnline;
        //    this.AudioUrlDowload = AudioUrlDowload;
        //    this.ImageURL = ImageURL;
        //    this.Transcript = Transcript;
        //    this.Actor = Actor;
        //    this.Vocabulary = Vocabulary;
        //    this.CreatedDate = CreatedDate;
        //    this.UpdatedDate = UpdatedDate;
        //    this.Sumary = Sumary;
        //}
    }
}
