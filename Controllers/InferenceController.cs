using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BYU_EGYPT_INTEX.Models;
using System.Xml;
using BYU_EGYPT_INTEX.Models.ViewModels;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using BYU_EGYPT_INTEX.Models.AnalyticsModels;


namespace BYU_EGYPT_INTEX.Controllers
{
    [ApiController]
    [Route("/score")]
    public class InferenceController : ControllerBase
    {
        private InferenceSession _session;

        public InferenceController()
        {
            _session = new InferenceSession("Models/AnalyticsModels/textilesupermodelfinal.onnx");
        }


        [HttpPost]
        public ActionResult Score(Models.AnalyticsModels.Data data)
        {
            var result = _session.Run(new List<NamedOnnxValue>
                {
                    NamedOnnxValue.CreateFromTensor("input", data.AsTensor())
                });

            Tensor<string> output_label = result.First().AsTensor<string>();
            var prediction = new Prediction { PredictedValue = output_label.First() };
            result.Dispose();
            return Ok(prediction);
        }
    }
        [ApiController]
        [Route("/score2")]
        public class InferenceController2 : ControllerBase
        {
            private InferenceSession _session;

            public InferenceController2()
            {
                _session = new InferenceSession("Models/AnalyticsModels/bmsupermodel2final.onnx");
            }

            [HttpPost]
            public ActionResult Score(Models.AnalyticsModels.Data2 data)
            {
                var result = _session.Run(new List<NamedOnnxValue>
                {
                    NamedOnnxValue.CreateFromTensor("input", data.AsTensor())
                });

                Tensor<string> output_label = result.First().AsTensor<string>();
                var prediction = new Prediction { PredictedValue = output_label.First() };
                result.Dispose();
                return Ok(prediction);
            }
        }
    
}