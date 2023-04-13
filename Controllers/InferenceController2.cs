//using System.Diagnostics;
//using Microsoft.AspNetCore.Mvc;
//using BYU_EGYPT_INTEX.Models;
//using System.Xml;
//using BYU_EGYPT_INTEX.Models.ViewModels;
//using static System.Reflection.Metadata.BlobBuilder;
//using BYU_EGYPT_INTEX.Data;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Drawing;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.ML.OnnxRuntime;
//using Microsoft.ML.OnnxRuntime.Tensors;
//using BYU_EGYPT_INTEX.Models.AnalyticsModels;


//namespace BYU_EGYPT_INTEX.Controllers
//{
//    [ApiController]
//    [Route("/score2")]
//    public class InferenceController2 : ControllerBase
//    {
//        private InferenceSession _session;

//        public InferenceController2(InferenceSession session)
//        {
//            _session = session;
//        }

//        [HttpPost]
//        public ActionResult Score(Models.AnalyticsModels.Data data)
//        {
//            var result = _session.Run(new List<NamedOnnxValue>
//                {
//                    NamedOnnxValue.CreateFromTensor("input", data.AsTensor())
//                });

//            Tensor<string> output_label = result.First().AsTensor<string>();
//            var prediction = new Prediction { PredictedValue = output_label.First() };
//            result.Dispose();
//            return Ok(prediction);


//            //var categories = new[] { "B", "H", "W" };
//            //int predictionIndex = Array.IndexOf(output_label.ToArray(), output_label.Max());
//            //var prediction = new Prediction2 { PredictedValue = categories[predictionIndex] };
//            //return Ok(prediction);
//        }

//    }

//}