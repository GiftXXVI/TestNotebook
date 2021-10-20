using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace TestNotebook.Models
{
    [Table("Header", Schema = "Tests")]
    public class Header
    {
        [Required]
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Detail> Details { get; set; }
    }
    [Table("Detail", Schema = "Tests")]
    public class Detail
    {
        [Required]
        public int Id { get; set; }
        public int HeaderId { get; set; }
        [ForeignKey("HeaderId")]
        public Header Header { get; set; }
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
        public bool AnswerYN { get; set; }
        public string AnswerText { get; set; }
        public int FieldId { get; set; }
        [ForeignKey("FieldId")]
        public Field Field { get; set; }
        public int ResultId { set; get; }
        [ForeignKey("ResultId")]
        public Result Result { get; set; }

    }
    [Table("Question", Schema = "Tests")]
    public class Question
    {
        [Required]
        public int Id { get; set; }
        public int ControlId { get; set; }
        [ForeignKey("ControlId")]
        public Control Control { get; set; }
        public string Description { get; set; }
        public ICollection<Detail> Details { get; set; }
    }
    [Table("Control", Schema = "Tests")]
    public class Control
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
    [Table("Screen", Schema = "Tests")]
    public class Screen
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Field> Fields { get; set; }
    }
    [Table("Field", Schema = "Tests")]
    public class Field
    {
        [Required]
        public int Id { get; set; }
        public int ScreenId { get; set; }
        [ForeignKey("ScreenId")]
        public Screen Screen { get; set; }
        public string Name { get; set; }
    }
    [Table("Result", Schema = "Tests")]
    public class Result
    {
        [Required]
        public int Id { get; set; }
        public string description { get; set; }
        public ICollection<Detail> Details { get; set; }
    }
}