using System.Collections;


using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;
namespace Com.MyCompany.HRGame{

    //플레이어 이름 입력 필드. 사용자가 자신의 이름을 입력하게 되면 게임의 플레이이어 위에 나타남.
     [RequireComponent(typeof(InputField))]
     public class PlayerNameInputField : MonoBehaviour
    {
      #region  Private Constants

    //오타를 방지하기 위해 플레이어 프리프 키 저장
      const string playerNamePrefKey = "PalyerName";

      #endregion

      #region MonoBehaviour CallBacks

    //MonoBehavior 메서드는 초기화 단계에서 Unity에 의해 GameObject를 호출
        void Start()
        {
            string defaultName = string.Empty;//  빈 문자열을 저장
           // 이 스크립트가 연결된 게임 개체에 연결된 InputField 구성 요소에 대한 참조를 얻으려고 시도
            InputField _inputField = this.GetComponent<InputField>();
            if(_inputField !=null)//널이 아닐때
            {
                // PlayerPrefs에 있는지 여부를 나타내는 부울 값 'true' 또는 'false'를 반환
                if (PlayerPrefs.HasKey(playerNamePrefKey))//즉, ture를 반환하면 이전에 플레이어 이름이 저장되었음
                {
                    //저장된 플레이어 이름을 defaultName 변수에 할당
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    //기본 플레이어 이름을 표시하도록  InputField 구성요소 _inputField의 텍스트를 설정
                    _inputField.text = defaultName;
                }
            }    
            // Photon Network 시스템에서 플레이어의 닉네임을 defaultName 값으로 설정                
             PhotonNetwork.NickName =  defaultName;
        }
        #endregion

        public void OnPlayerNameValueChanged(string value)
        {
            SetPlayerName(value);
        }

        //플레이어의 이름을 설정하고 이후 세션을 위해 플레이어 기본 설정에 저장
        #region Public Methods
        // <param name="value">The name of the Player</param>
        public void SetPlayerName(string value)
        {

            if (string.IsNullOrEmpty(value)) // 사용자로부터 입력 받은 값이 빈 문자열인지 확인
            {
                Debug.LogError("Player Name is null or empty");
                return;

            }
            //Photon Network 시스템에서 플레이어의 닉네임을 제공된 value로 설정
            PhotonNetwork.NickName = value;

            PlayerPrefs.SetString(playerNamePrefKey,value);
        }

        #endregion

    }

}