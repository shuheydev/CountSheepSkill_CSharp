using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace CountSheepSkill_CSharp
{
    public class Function
    {
        private readonly string skillName = "羊カウント";


        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="skillRequest"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public SkillResponse FunctionHandler(SkillRequest skillRequest, ILambdaContext context)
        {
            SkillResponse skillResponse = null;

            try
            {
                switch (skillRequest.Request)
                {
                    case LaunchRequest launchRequest:
                        skillResponse = LaunchRequestHandler(skillRequest);
                        break;
                    case IntentRequest intentRequest:
                        switch (intentRequest.Intent.Name)
                        {
                            case "StartCountSheepIntent":
                                skillResponse =StartCountSheepIntentHandler(skillRequest);
                                break;
                            case "StopCountSheepIntent":
                                skillResponse = StopCountSheepIntentHandler(skillRequest);
                                break;
                            case "AMAZON.HelpIntent":
                                skillResponse = HelpIntentHandler(skillRequest);
                                break;
                            case "AMAZON.CancelIntent":
                                skillResponse = CancelAndStopIntentHandler(skillRequest);
                                break;
                            case "AMAZON.StopIntent":
                                skillResponse = CancelAndStopIntentHandler(skillRequest);
                                break;
                            default:
                                //skillResponse = ErrorHandler(skillRequest);
                                break;
                        }

                        break;
                    case SessionEndedRequest sessionEndedRequest:
                        skillResponse = SessionEndedRequestHandler(skillRequest);
                        break;
                    default:
                        //skillResponse = ErrorHandler(skillRequest);
                        break;
                }
            }
            catch (Exception ex)
            {
                skillResponse = ErrorHandler(skillRequest);
            }

            return skillResponse;
        }




        #region 各インテント、リクエストに対応する処理を担当するメソッドたち

        private SkillResponse LaunchRequestHandler(SkillRequest skillRequest)
        {
            var launchRequest = skillRequest.Request as LaunchRequest;

            var speechText = "羊を1から100までカウントします。"+
                "カウントを開始しますか？";

            var skillResponse = new SkillResponse
            {
                Version = "1.0",
                Response = new ResponseBody()
            };

            skillResponse.Response.OutputSpeech = new PlainTextOutputSpeech
            {
                Text = speechText
            };
            skillResponse.Response.Reprompt = new Reprompt
            {
                OutputSpeech = new PlainTextOutputSpeech
                {
                    Text = speechText
                }
            };
            skillResponse.Response.Card = new SimpleCard
            {
                Title =skillName,
                Content = speechText
            };

            return skillResponse;
        }

        //ご自分で追加したインテントに合わせて名前や処理を変更してください。
        private SkillResponse StartCountSheepIntentHandler(SkillRequest skillRequest)
        {
            var intentRequest = skillRequest.Request as IntentRequest;

            var speechText = "羊を100までカウントします。";

            var skillResponse = new SkillResponse
            {
                Version = "1.0",
                Response = new ResponseBody()
            };

            var breakTime =1.0;

            var countContents = ComposeMessage.ComposeCountContents(breakTime);

            //SSMLを利用する。
            //読み上げの間を調整するため
            skillResponse.Response.OutputSpeech = new SsmlOutputSpeech
            {
                Ssml =$"<speak>{speechText+countContents}。カウントを終わります。</speak>"
            };
            skillResponse.Response.Card = new SimpleCard
            {
                Title = skillName,
                Content = speechText
            };
            skillResponse.Response.ShouldEndSession = true;

            return skillResponse;
        }

        private SkillResponse StopCountSheepIntentHandler(SkillRequest skillRequest)
        {
            var intentRequest = skillRequest.Request as IntentRequest;

            var speechText = "カウントを終わります。";

            var skillResponse = new SkillResponse
            {
                Version = "1.0",
                Response = new ResponseBody()
            };

            var breakTime = 1.0;

            var countContents = ComposeMessage.ComposeCountContents(breakTime);


            skillResponse.Response.OutputSpeech = new PlainTextOutputSpeech
            {
                Text = speechText
            };
            skillResponse.Response.Card = new SimpleCard
            {
                Title = skillName,
                Content = speechText
            };
            skillResponse.Response.ShouldEndSession = true;

            return skillResponse;
        }

        /// <summary>
        /// 組み込みインテント用
        /// </summary>
        /// <param name="skillRequest"></param>
        /// <returns></returns>
        private SkillResponse HelpIntentHandler(SkillRequest skillRequest)
        {
            var intentRequest = skillRequest.Request as IntentRequest;

            var speechText = "羊を100までカウントします。"+
                "例えば、スキルを起動したら、カウントを開始する場合は、はい、を。" +
                "カウントを開始しない場合は、いいえ、と言ってください。"; 

            var skillResponse = new SkillResponse
            {
                Version = "1.0",
                Response = new ResponseBody()
            };

            skillResponse.Response.OutputSpeech = new PlainTextOutputSpeech
            {
                Text = speechText
            };
            skillResponse.Response.Reprompt = new Reprompt
            {
                OutputSpeech = new PlainTextOutputSpeech
                {
                    Text = speechText
                }
            };
            skillResponse.Response.Card = new SimpleCard
            {
                Title = skillName,
                Content = speechText
            };

            return skillResponse;
        }


        /// <summary>
        /// 組み込みインテント用
        /// </summary>
        /// <param name="skillRequest"></param>
        /// <returns></returns>
        private SkillResponse CancelAndStopIntentHandler(SkillRequest skillRequest)
        {
            var intentRequest = skillRequest.Request as IntentRequest;

            var speechText = "カウントを終わります。";

            var skillResponse = new SkillResponse
            {
                Version = "1.0",
                Response = new ResponseBody()
            };

            skillResponse.Response.OutputSpeech = new PlainTextOutputSpeech
            {
                Text = speechText
            };
            skillResponse.Response.Card = new SimpleCard
            {
                Title = skillName,
                Content = speechText
            };
            skillResponse.Response.ShouldEndSession = true;

            return skillResponse;
        }

        /// <summary>
        /// 組み込みインテント用
        /// </summary>
        /// <param name="skillRequest"></param>
        /// <returns></returns>
        private SkillResponse SessionEndedRequestHandler(SkillRequest skillRequest)
        {
            var skillResponse = new SkillResponse
            {
                Version = "1.0",
                Response = new ResponseBody()
            };

            skillResponse.Response.OutputSpeech = new PlainTextOutputSpeech
            {
                Text = "カウントを終わります。"
            };

            return skillResponse;
        }

        /// <summary>
        /// 組み込みインテント用
        /// </summary>
        /// <param name="skillRequest"></param>
        /// <returns></returns>
        private SkillResponse ErrorHandler(SkillRequest skillRequest)
        {
            var speechText = "すみません、よく聞こえませんでした。";

            var skillResponse = new SkillResponse
            {
                Version = "1.0",
                Response = new ResponseBody()
            };

            skillResponse.Response.OutputSpeech = new PlainTextOutputSpeech
            {
                Text = speechText
            };
            skillResponse.Response.Reprompt = new Reprompt
            {
                OutputSpeech = new PlainTextOutputSpeech
                {
                    Text = speechText
                }
            };
            skillResponse.Response.ShouldEndSession = true;//エラーなのでセッションを終了させなければならない。

            return skillResponse;
        }

        #endregion

    }
}
