using System;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace BYU_EGYPT_INTEX.Models.AnalyticsModels
{
	public class Data
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
        public float preservation_skeletalized { get; set; }
        public float preservation_well_preserved { get; set; }
        public float ageatdeath_ { get; set; }
        public float ageatdeath_A { get; set; }
        public float ageatdeath_C { get; set; }
        public float ageatdeath_I { get; set; }
        public float ageatdeath_N { get; set; }
        public float textilefunctionvalue_Blanket_shroud { get; set; }
        public float textilefunctionvalue_Fragment { get; set; }
        public float textilefunctionvalue_Other { get; set; }
        public float textilefunctionvalue_Ribbon { get; set; }
        public float textilefunctionvalue_Tunic { get; set; }
        public float angle_ { get; set; }
        public float angle_H { get; set; }
        public float angle_M { get; set; }
        public float angle_M_H { get; set; }
        public float angle_M_H_C { get; set; }
        public float angle_S { get; set; }
        public float angle_S_M { get; set; }
        public float manipulation_Other { get; set; }
        public float manipulation_Warp { get; set; }
        public float manipulation_Weft { get; set; }
        public float material_Linen { get; set; }
        public float material_Other { get; set; }
        public float material_Wool { get; set; }
        public float direction_ { get; set; }
        public float direction_S { get; set; }
        public float direction_Z { get; set; }
        public float direction_Z_S { get; set; }

        public Tensor<float> AsTensor()
        {
            float[] data = new float[]
            {
                depth, headdirection_, headdirection_E, headdirection_W, adultsubadult_, adultsubadult_A, adultsubadult_C, preservation_, preservation_bones, preservation_deteriorated, preservation_skeletalized, preservation_well_preserved, ageatdeath_, ageatdeath_A, ageatdeath_C, ageatdeath_I, ageatdeath_N, textilefunctionvalue_Blanket_shroud, textilefunctionvalue_Fragment, textilefunctionvalue_Other, textilefunctionvalue_Ribbon, textilefunctionvalue_Tunic, angle_, angle_H, angle_M, angle_M_H, angle_M_H_C, angle_S, angle_S_M, manipulation_Other, manipulation_Warp, manipulation_Weft, material_Linen, material_Other, material_Wool, direction_, direction_S, direction_Z, direction_Z_S
            };
            int[] dimensions = new int[] { 1, 39 };
            return new DenseTensor<float>(data, dimensions);
        }
    }
}

