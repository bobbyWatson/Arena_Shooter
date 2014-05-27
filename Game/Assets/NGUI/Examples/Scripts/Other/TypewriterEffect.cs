using UnityEngine;

/// <summary>
/// Trivial script that fills the label's contents gradually, as if someone was typing.
/// </summary>

[RequireComponent(typeof(UILabel))]
[AddComponentMenu("NGUI/Examples/Typewriter Effect")]
public class TypewriterEffect : MonoBehaviour
{
	public int charsPerSecond = 40;
	public GameObject endEventReceiver;
	public string endEventFunctionName;

	UILabel mLabel;
	string mText;
	int mOffset = 0;
	float mNextChar = 0f;

	void Update ()
	{
		if (mLabel == null)
		{
			mLabel = GetComponent<UILabel>();
			mLabel.supportEncoding = false;
			mLabel.symbolStyle = NGUIText.SymbolStyle.None;
			mText = mLabel.processedText;
		}

		if (mOffset < mText.Length)
		{
			if (mNextChar <= RealTime.time)
			{
				charsPerSecond = Mathf.Max(1, charsPerSecond);

				// Periods and end-of-line characters should pause for a longer time.
				float delay = 1f / charsPerSecond;
				char c = mText[mOffset];
				if (c == '.' || c == '\n' || c == '!' || c == '?') delay *= 4f;

				mNextChar = RealTime.time + delay;
				mLabel.text = mText.Substring(0, ++mOffset);
			}
		}
		else{
			if(endEventReceiver != null)
				endEventReceiver.SendMessage(endEventFunctionName,SendMessageOptions.DontRequireReceiver);
			Destroy(this);
		}
	}

	public void End(){
		if(mLabel == null)
			return;
		mLabel.text = mText;
		if(endEventReceiver != null)
			endEventReceiver.SendMessage(endEventFunctionName,SendMessageOptions.DontRequireReceiver);
		Destroy(this);
	}
}
