using MailHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailHelper
{
    /// <summary>
    /// 제  목: Mailer
    /// 작성자: sadtimez
    /// 작성일: 2019-06-21
    /// 설  명: Mailer 추상 클래스
    /// 수정자: 수정자<탭>수정내용<탭>수정일자
    /// </summary>
    public class Mailer
    {
        public event Action<string, string, string, string> Success; // 메일 성공시 추가 동작, Ex) 로그 남기기
        public event Action<string, string, string, Exception> Fail; // 메일 실패시 추가 동작, Ex) 로그 남기기
        public event Action<Dictionary<string, string>, string, string, string> SuccessDictionary; // 메일 성공시 추가 동작, Ex) 로그 남기기
        public event Action<Dictionary<string, string>, string, string, Exception> FailDictionary; // 메일 실패시 추가 동작, Ex) 로그 남기기

        private Task looper = null;
        private Queue<Action> queue = new Queue<Action>();
        private SemaphoreSlim locker = new SemaphoreSlim(1);

        // 로그 남기기
        public void Send(Dictionary<string, string> email, string subject, string body)
        {
            Enqueue(() =>
            {
                try
                {
                    string response = SendProcess(email, subject, body);
                    ExcuteSuccess(email, subject, body, response);
                }
                catch (Exception ex)
                {
                    ExcuteFail(email, subject, body, ex);
                }
            });
        }

        // 로그 남기기
        public void Send(string email, string subject, string body)
        {
            Enqueue(() =>
            {
                try
                {
                    string response = SendProcess(email, subject, body);
                    ExcuteSuccess(email, subject, body, response);
                }
                catch (Exception ex)
                {
                    ExcuteFail(email, subject, body, ex);
                }
            });
        }

        private void Enqueue(Action action)
        {
            try
            {
                locker.Wait();
                queue.Enqueue(action);
                if (looper == null)
                {
                    looper = Task.Run(() =>
                    {
                        Loop();
                    });
                }
            }
            catch { }
            finally
            {
                locker.Release();
            }
        }

        private void Loop()
        {
            while (true)
            {
                Action action = null;
                try
                {
                    locker.Wait();
                    if (queue.Count > 0)
                    {
                        action = queue.Dequeue();
                    }
                    else
                    {
                        looper = null;
                        break;
                    }
                }
                catch { }
                finally
                {
                    locker.Release();
                }
                action();
            }
        }

        #region 내부함수

        // 이벤트 동작
        private void ExcuteSuccess(string email, string subject, string body, string response)
        {
            if (Success != null)
            {
                Success(email, subject, body, response);
            }
        }
        // 이벤트 동작
        private void ExcuteSuccess(Dictionary<string, string> email, string subject, string body, string response)
        {
            if (Success != null)
            {
                SuccessDictionary(email, subject, body, response);
            }
        }

        // 이벤트 동작
        private void ExcuteFail(string email, string subject, string body, Exception ex)
        {
            if (Fail != null)
            {
                Fail(email, subject, body, ex);
            }
        }
        // 이벤트 동작
        private void ExcuteFail(Dictionary<string, string> email, string subject, string body, Exception ex)
        {
            if (Fail != null)
            {
                FailDictionary(email, subject, body, ex);
            }
        }


        // 구현
        protected virtual string SendProcess(string email, string subject, string body)
        {
            string ret = string.Empty;

            return ret;
        }

        // 구현
        protected virtual string SendProcess(Dictionary<string, string> email, string subject, string body)
        {
            string ret = string.Empty;

            return ret;
        }
        #endregion
    }
}
