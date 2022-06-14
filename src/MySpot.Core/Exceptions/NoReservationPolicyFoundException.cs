using MySpot.Core.ValueObjects;

namespace MySpot.Core.Exceptions
{
    public sealed class NoReservationPolicyFoundException : CustomException
    {
        public NoReservationPolicyFoundException(JobTitle jobTitle) 
            : base($"No reservation policy for the job title {jobTitle} found.")
        {
            JobTitle = jobTitle;
        }

        public JobTitle JobTitle { get; }
    }
}
