using MyApiTest.Models;

namespace MyApiTest.Interfaces
{
    /// <summary>
    /// Interface for Trolley Service
    /// </summary>
    public interface ITrolleyService
    {
        /// <summary>
        /// Calculates the minimum total for the trolley request.
        /// </summary>
        /// <param name="trolleyRequest">The trolley request object.</param>
        /// <returns>Minimum trolley amount.</returns>
        double CalculateMinimumTotal(TrolleyRequest trolleyRequest);
    }
}
