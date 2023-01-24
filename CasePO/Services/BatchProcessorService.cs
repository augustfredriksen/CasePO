
namespace CasePO.Services
{
    /// <summary>
    /// BatchProcessorService class is used to create batches from the list of strings.
    /// </summary>
    public class BatchProcessorService
    {
        /// <summary>
        /// Creates a list of batches from a list of orgNos, each batch has a maximum size of batchSize.
        /// The brønnøysundregister API only supports a maximum of 10000 results per request,
        /// by creating batches we will make sure to never exceed the maximum results per request.
        /// </summary>
        /// <param name="orgNos"> A list of orggNos that needs to be divided into batches </param>
        /// <param name="batchSize"> The maximum size of each batch </param>
        /// <returns> A list of list of orgNos where each list represents a batch </returns>
        public List<List<string>> CreateBatches(List<string> orgNos, int batchSize)
        {
            var batches = new List<List<string>>();

            // Iterate over the list of orgNos to create batches
            for (int i = 0; i < orgNos.Count; i += batchSize)
            {
                // Add the orgNos range of current batch into list. The Math.Min makes sure the last batch size does not exceed the total orgNos count.
                batches.Add(orgNos.GetRange(i, Math.Min(batchSize, orgNos.Count - i)));
            }
            return batches;
        }
    }
}
