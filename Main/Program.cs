using Util;

var schedule = new Interval(12, 18);
var extratime = new Interval(18, 22);

var totalTime = schedule.Merged(extratime);

Console.WriteLine(totalTime.Length());



