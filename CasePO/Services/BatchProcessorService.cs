
namespace CasePO.Services
{
    public class BatchProcessorService
    {
        public List<List<string>> CreateBatches(List<string> orgNos, int batchSize)
        {
            var batches = new List<List<string>>();

            for (int i = 0; i < orgNos.Count; i += batchSize)
            {
                batches.Add(orgNos.GetRange(i, Math.Min(batchSize, orgNos.Count - i)));
            }

            return batches;
        }
    }
}
