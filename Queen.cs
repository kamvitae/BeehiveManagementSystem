using System;
using System.ComponentModel;

namespace HFIV_6_BeehiveManagementSystem
{
    class Queen : Bee, INotifyPropertyChanged
    {
        const float EGGS_PER_SHIFT = 0.45f;
        const float HONEY_PER_UNASSIGNED_WORKER = 0.5f;

        private IWorker[] workers = new IWorker[0];
        private float eggs = 0;
        private float unasignedWorkers = 3;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string StatusReport { get; private set; }
        protected override float CostPerShift { get { return 2.15f; } }

        public Queen() : base("Królowa")
        {
            unasignedWorkers = 3;
            AssignBee("Zbieraczka nektaru");
            AssignBee("Producentka miodu");
            AssignBee("Opiekunka jaj");
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void DoJob()
        {
            eggs += EGGS_PER_SHIFT;
            foreach (Bee worker in workers)
            {
                worker.WorkNextShift();
            }
            HoneyVault.ConsumeHoney(HONEY_PER_UNASSIGNED_WORKER * unasignedWorkers);
            UpdateStatusReport();
        }

        /// <summary>
        /// Tworzy i przydziela robotnicę do zadania. Na koniec aktualizuje raport.
        /// </summary>
        /// <param name="job">Zadanie nowoutworzonej robotnicy.</param>
        public void AssignBee(string job)
        {
            switch (job)
            {
                case "Zbieraczka nektaru":
                    AddWorker(new NectarCollector());
                    break;
                case "Producentka miodu":
                    AddWorker(new HoneyManufacturer());
                    break;
                case "Opiekunka jaj":
                    AddWorker(new EggCare(this));
                    break;
            }
            UpdateStatusReport();
        }

        /// <summary>
        /// Zwiększanie tablicy workers o jedno miejsce i dodanie referencji typu Bee
        /// </summary>
        /// <param name="worker">Robotnica dodawana do tablicy workers</param>
        private void AddWorker(IWorker worker)
        {
            if (unasignedWorkers >= 1)
            {
                unasignedWorkers--;
                Array.Resize(ref workers, workers.Length + 1);
                workers[workers.Length - 1] = worker;
                //może być zapisane jako:
                // workers[^1] = worker;
            }
        }

        /// <summary>
        /// Konwertuje jaja w pszczoły bez przydzielonego zadania.
        /// </summary>
        /// <param name="eggsToConvert">Ilośc jaj podlegających konwersji w pszczoły.</param>
        public void CareForEggs(float eggsToConvert)
        {
            if (eggs >= eggsToConvert)
            {
                eggs -= eggsToConvert;
                unasignedWorkers += eggsToConvert;
            }
        }

        /// <summary>
        /// Pobiera dane ze statycznej klasy HoneyVault i uzupełnia je o dane pracownic./// </summary>
        /// <returns>Pełen raport zmiany dotyczący ula.</returns>
        private void UpdateStatusReport()
        {
            StatusReport = $"{HoneyVault.StatusReport}\n" +
                $"Liczba jaj: {eggs:0.0}\n" +
                $"Nieprzydzielone robotnice: {unasignedWorkers:0.0}\n" +
                $"{WorkerStatus("Zbieraczka nektaru")}{WorkerStatus("Producentka miodu")}" +
                $"{WorkerStatus("Opiekunka jaj")}\nROBOTNICE W SUMIE:\t{workers.Length}";
            OnPropertyChanged("StatusReport");
        }
        /// <summary>
        /// Wylicza obecną liczbę pracownic wskazanego typu.
        /// </summary>
        /// <param name="job">Nazwa zawodu pracownicy ula.</param>
        /// <returns>Ciąg znaków wskazujący liczbę pracownic</returns>
        private string WorkerStatus(string job)
        {
            int count = 0;
            foreach (IWorker worker in workers)
            {
                if (worker.Job == job)
                    count++;
            }
            return $"\n{job}:\t{count}";
        }
    }
}
