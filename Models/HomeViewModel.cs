using System;
using Blog.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Models;

public class HomeViewModel
{
    public List<Question>? Questions { get; set; }
    public Comment? Comment { get; set; }
    public string? Search {get;set;}
    public string? Nick {get;set;}

    public string TimeCalculator(TimeSpan Time)
    {
        int TotalSecond = Convert.ToInt32(Time.TotalSeconds);
        int Second = 1; int Minute = Second * 60; int Hour = Minute * 60; int Day = Hour * 24; int Mounth = Day * 30; int Year = Mounth * 12;

        if (TotalSecond < Minute)
        {
            return "az önce";
        }
        else if (TotalSecond < Hour)
        {
            return TotalSecond / Minute + " dakika önce";
        }
        else if (TotalSecond < Day)
        {
            return TotalSecond / Hour + " saat önce";
        }
        else if (TotalSecond < Mounth)
        {
            return TotalSecond / Day + " gün önce";
        }
        else if (TotalSecond < Year)
        {
            return TotalSecond / Mounth + " ay önce";
        }
        else
        {
            return TotalSecond / Year + " yıl önce";
        }
    }
}
