using System.Diagnostics;

namespace Util;

public class Interval
{
    private double _start;
    private double _end;

    public Interval()
    {
        setValues(0, 0);
    }

    public Interval(double start, double end)
    {
        Debug.Assert(0 <= _end);
        setValues(start, end);
    }

    public Interval(double end) :
        this(0, end)
    {

    }

    public Interval(Interval interval) :
        this(interval._start, interval._end)
    {

    }

    public double Length()
    {
        return _end - _start;
    }

    public Interval Clone()
    {
        return new Interval(_start, _end);
    }

    public void moveTo(double value)
    {
        _start += value;
        _end += value;
    }

    public Interval moved(double value)
    {
        var moved = Clone();
        moved.moveTo(value);
        return moved;
    }

    public bool Contains(double value)
    {
        return _start <= value && value <= _end;
    }

    public bool Contains(Interval interval)
    {
        Debug.Assert(interval is not null);
        return Contains(interval._start) &&
            Contains(interval._end);
    }

    public bool Equals(Interval interval)
    {
        Debug.Assert(interval is not null);
        return _start == interval._start
            && _end == interval._end;
    }

    public void Double()
    {
        var length = Length();
        _start -= length / 2;
        _end += length / 2;
    }

    public Interval Intersection(Interval interval)
    {
        Debug.Assert(HasIntersection(interval));
        if (Contains(interval))
        {
            return interval.Clone();
        }
        else if (interval.Contains(this))
        {
            return this.Clone();
        }
        else if (this.Contains(interval._start))
        {
            return new Interval(interval._start, _end);
        }
        else
        {
            return new Interval(_start, interval._end);
        }
    }

    public bool HasIntersection(Interval interval)
    {
        return Contains(interval._start) ||
            Contains(interval._end) ||
            Contains(interval);
    }

    public Interval Merged(Interval interval)
    {
        Debug.Assert(HasIntersection(interval));
        return new Interval(Math.Min(_start , interval._start) , Math.Max(_end , interval._end));
    }

    public void Merge(Interval interval)
    {
        Debug.Assert(HasIntersection(interval));
        setValues(Math.Min(_start, interval._start), Math.Max(_end, interval._end));
    }

    public void Show()
    {
        Console.WriteLine("-------------");
        Console.WriteLine($"start: {_start}, end: {_end}");
        Console.WriteLine("-------------");

    }

    private void setValues(double start, double end)
    {
        _start = start;
        _end = end;
    }
}
