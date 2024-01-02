using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFIV_6_BeehiveManagementSystem
{

    abstract class Bee : IWorker
    {

        public string Job { get;}
        protected abstract float CostPerShift { get; }
        //private float honeyConsumed = CostPerShift;
        public Bee(string job)
        {
            Job = job;
        }

        /// <summary>
        /// 
        /// </summary>
        public void WorkNextShift()
        {
            if (HoneyVault.ConsumeHoney(CostPerShift))
                DoJob();
        }
        /// <summary>
        /// 
        /// </summary>
        protected abstract void DoJob();
    }

    class NectarCollector : Bee
    {
        /// <summary>
        /// Tworzy obiekt typy NectarCollector i przydziela 
        /// </summary>
        /// <param name="job"><param name="job">Bee job description. Should be "Zbieraczka nektaru".</param>
        public NectarCollector() : base("Zbieraczka nektaru") { }

        protected override float CostPerShift { get { return 1.95f; } }
        const float NECTAR_COLLECTED_PER_SHIFT = 33.25f;

        protected override void DoJob()
        {
            HoneyVault.CollectNectar(NECTAR_COLLECTED_PER_SHIFT);
        }
    }

    class HoneyManufacturer : Bee
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"><param name="job">Bee job description. Should be "Producentka miodu".</param>
        public HoneyManufacturer() : base("Producentka miodu") { }

        protected override float CostPerShift { get { return 1.7f; } }
        const float NECTAR_PROCESSED_PER_SHIFT = 33.15f;

        protected override void DoJob()
        {
            HoneyVault.ConvertNectarToHoney(NECTAR_PROCESSED_PER_SHIFT);
        }
    }

    class EggCare : Bee
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="job">Bee job description. Should be "Opiekunka jaj".</param>
        public EggCare(Queen queen) : base("Opiekunka jaj")
        {
            this.queen = queen;
        }

        private Queen queen;
        protected override float CostPerShift { get { return 1.35f; } }
        const float CARE_PROGRESS_PER_SHIFT = 0.15f;

        protected override void DoJob()
        {
            queen.CareForEggs(CARE_PROGRESS_PER_SHIFT);
        }
    }
}
