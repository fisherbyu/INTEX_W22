using System;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace BYU_EGYPT_INTEX.Models.AnalyticsModels
{
	public class Data2
    {
        public float depth { get; set; }
        public float headdirection_ { get; set; }
        public float headdirection_E { get; set; }
        public float headdirection_W { get; set; }
        public float adultsubadult_ { get; set; }
        public float adultsubadult_A { get; set; }
        public float adultsubadult_C { get; set; }
        public float preservation_ { get; set; }
        public float preservation_bones { get; set; }
        public float preservation_deteriorated { get; set; }
        public float preservation_fairly_well_preserved { get; set; }
        public float preservation_skeletalized { get; set; }
        public float preservation_very_well_preserved { get; set; }
        public float preservation_well_preserved { get; set; }
        public float samplescollected_ { get; set; }
        public float samplescollected_true { get; set; }
        public float samplescollected_false { get; set; }
        public float ageatdeath_ { get; set; }
        public float ageatdeath_A { get; set; }
        public float ageatdeath_C { get; set; }
        public float ageatdeath_I { get; set; }
        public float ageatdeath_N { get; set; }

        public Tensor<float> AsTensor()
        {
            float[] data = new float[]
            {
                depth, headdirection_, headdirection_E, headdirection_W, adultsubadult_, adultsubadult_A, adultsubadult_C, preservation_, preservation_bones, preservation_deteriorated, preservation_fairly_well_preserved, preservation_skeletalized, preservation_very_well_preserved, preservation_well_preserved, samplescollected_, samplescollected_true, samplescollected_false, ageatdeath_, ageatdeath_A, ageatdeath_C, ageatdeath_I, ageatdeath_N
            };
            int[] dimensions = new int[] { 1, 22 };
            return new DenseTensor<float>(data, dimensions);
        }
    }
}

