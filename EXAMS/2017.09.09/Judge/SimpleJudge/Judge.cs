using System;
using System.Collections.Generic;
using System.Linq;

public class Judge : IJudge
{
    private readonly HashSet<int> contests;
    private readonly HashSet<int> users;
    private readonly Dictionary<int, Submission> submissions;

    public Judge()
    {
        this.contests = new HashSet<int>();
        this.users = new HashSet<int>();
        this.submissions = new Dictionary<int, Submission>();
    }

    public void AddContest(int contestId)
    {
        this.contests.Add(contestId);
    }

    public void AddSubmission(Submission submission)
    {
        if (this.submissions.ContainsKey(submission.Id))
        {
            return;
        }

        if (!this.users.Contains(submission.UserId) || !this.contests.Contains(submission.ContestId))
        {
            throw new InvalidOperationException();
        }

        this.submissions.Add(submission.Id, submission);
    }

    public void AddUser(int userId)
    {
        this.users.Add(userId);
    }

    public void DeleteSubmission(int submissionId)
    {
        if (!this.submissions.ContainsKey(submissionId))
        {
            throw new InvalidOperationException();
        }

        this.submissions.Remove(submissionId);
    }

    public IEnumerable<int> GetContests()
    {
        return this.contests.OrderBy(c => c);
    }

    public IEnumerable<Submission> GetSubmissions()
    {
        return this.submissions.Values.OrderBy(s => s.Id);
    }

    public IEnumerable<int> GetUsers()
    {
        return this.users.OrderBy(u => u);
    }

    public IEnumerable<int> ContestsBySubmissionType(SubmissionType submissionType)
    {
        return this.submissions.Values.Where(s => s.Type.Equals(submissionType)).Select(s => s.ContestId).Distinct();
    }

    public IEnumerable<Submission> SubmissionsInContestIdByUserIdWithPoints(int points, int contestId, int userId)
    {
        var result = this.submissions.Values.Where(s => s.UserId.Equals(userId) && s.Points.Equals(points) && s.ContestId.Equals(contestId));

        if (result.Any())
        {
            return result;
        }

        throw new InvalidOperationException();
    }

    public IEnumerable<Submission> SubmissionsWithPointsInRangeBySubmissionType(int minPoints, int maxPoints, SubmissionType submissionType)
    {
        return this.submissions.Values.Where(s => s.Type.Equals(submissionType) && s.Points >= minPoints && s.Points <= maxPoints);
    }

    public IEnumerable<int> ContestsByUserIdOrderedByPointsDescThenBySubmissionId(int userId)
    {
        return this.submissions.Values.Where(s => s.UserId.Equals(userId)).OrderByDescending(s => s.Points)
            .ThenBy(s => s.Id).Select(s => s.ContestId).Distinct();
    }
}