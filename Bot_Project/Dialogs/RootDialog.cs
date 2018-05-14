using System;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot_Project.Dialogs
{
    [Serializable]

    public class RootDialog : IDialog<object>
    {
        internal class ChatBot
        {
            private IDialogContext context;
            private IAwaitable<object> result;

            public async Task Chatbot(IDialogContext context, IAwaitable<object> result)
            {
                this.context = context;
                this.result = result;

                var activity = await result as Activity; // 사용자 대화 값

                string str = "";
                string newline = str.Replace(Environment.NewLine, "\n"); // 줄바꿈
                // int length = (activity.Text ?? string.Empty).Length; // 문자열의 길이

                if (activity.Text.Contains("안녕") || activity.Text.Contains("반가")) // 텍스트가 Hello 일때
                {
                    await context.PostAsync("반가워요"); // 답장
                }
                else if (activity.Text.Contains("상무") || activity.Text.Contains("상문")) // 텍스트가 Hello 일때
                {
                    await context.PostAsync("안녕하세요 문씨"); // 답장
                }
                else if (activity.Text == "Hello") // 텍스트가 Hello 일때
                {
                    await context.PostAsync($"Hi, Sangmoo"); // 답장 , $는 유니코드로 나타내기
                }
                else if (activity.Text == "공지사항")
                {
                    // 바로 페이지로 이동
                    Process.Start("http://portal.hansei.ac.kr/portal/default/gnb/hanseiTidings/notice.page");
                }
                else if (activity.Text.Contains("기우"))
                {
                    await context.PostAsync("기우씨 안녕하세요");
                }
                else if (activity.Text.Contains("영빈") || activity.Text.Contains("앵비"))
                {
                    await context.PostAsync("반가워요 앵비씨");
                }
                else if (activity.Text == "Hi, MinJeong")
                {
                    await context.PostAsync("Hi, MinJeong! How are You?");
                }
                else if (activity.Text.Contains("네이버"))
                {
                    await context.PostAsync("http://www.naver.com 으로 이동"); // 링크 걸어주기
                }
                else if (activity.Text.Contains("페이스북"))
                {
                    Process.Start("http://www.facebook.com");
                }
                else if (activity.Text.Contains("Kiwoo")) // Kiwoo라는 단어가 들어가기만하면 실행
                {
                    Process.Start("http://www.facebook.com");
                }
                // return our reply to the user
                else
                {
                    // await context.PostAsync("You sent {activity.Text} which was {length} characters"); // 길이 반환
                    await context.PostAsync("시험용입니다  \n  Hello, 공지사항, 네이버, 페이스북 단어만 사용해주세요");
                }

            }
        }
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            ChatBot CB = new ChatBot(); // 
            await CB.Chatbot(context, result); // await 대화 대기 작업
            
            context.Wait(MessageReceivedAsync);
        }
    }
}
