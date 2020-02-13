using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using UnityEngine.UI;

public class LeapMotionScript : MonoBehaviour {

    public Text polaczenie, litera; // tworzę zmienne, w których będę przechowywać informacje o połączeniu i pokazywanej literze
    Controller controller; // tworzę zmienną mojego kontrolera LeapMotion

    Vector SelectFingerAndBone(List<Finger> fingers, string fingerName, ushort boneIndex) // tworzę funkcję wybierającą Palec i Kość
    {
        Finger finger = null;
        if (fingerName.Equals("kciuk")) // przypisuję kość o indexie 0 do nazwy "kciuk"
            finger = fingers[0];
        else if (fingerName.Equals("wskazujacy"))
            finger = fingers[1];
        else if (fingerName.Equals("srodkowy"))
            finger = fingers[2];
        else if (fingerName.Equals("serdeczny"))
            finger = fingers[3];
        else if (fingerName.Equals("maly"))
            finger = fingers[4];

        Bone bone = null;
        if (boneIndex == 0)
            bone = finger.Bone(Bone.BoneType.TYPE_METACARPAL); // przypisuję typ kości Metacarpal do pierwszej kości w palcu od nadgarstka
        else if (boneIndex == 1)
            bone = finger.Bone(Bone.BoneType.TYPE_PROXIMAL);
        else if (boneIndex == 2)
            bone = finger.Bone(Bone.BoneType.TYPE_INTERMEDIATE);
        else if (boneIndex == 3)
            bone = finger.Bone(Bone.BoneType.TYPE_DISTAL);

        return bone.Center; // zwracam wektor współrzędnych środkowego punktu kostki
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		controller = new Controller(); // tworzę nowy obiekt controller, czyli mój LeapMotion
        if (controller.IsConnected)
        { // sprawdzam, czy kontroler jest połączony, i jeśli jest to wykonuje instrukcje
            polaczenie.text = "Polaczony"; // wyświetlam napis, jesli jest połączenie

            Frame frame = controller.Frame(); // tworzę obiekt Frame i przepisuję do niego wartosc z metody Controller.Frame()
            List<Hand> hands = frame.Hands; // tworzę listę rąk wczytanych przez kontroler

            Vector k0, k1, k2, k3, w0, w1, w2, w3, sr0, sr1, sr2, sr3, s0, s1, s2, s3, m0, m1, m2, m3; // tworzę zmienne wektorowe do przechowywania współrzędnych danej kostki.

            k0 = SelectFingerAndBone(hands[0].Fingers, "kciuk", 0); // wczytuję rękę 1, palec o aliasie "kciuk" i jego pierwszą kość.
            k1 = SelectFingerAndBone(hands[0].Fingers, "kciuk", 1);
            k2 = SelectFingerAndBone(hands[0].Fingers, "kciuk", 2);
            k3 = SelectFingerAndBone(hands[0].Fingers, "kciuk", 3);

            w0 = SelectFingerAndBone(hands[0].Fingers, "wskazujacy", 0);
            w1 = SelectFingerAndBone(hands[0].Fingers, "wskazujacy", 1);
            w2 = SelectFingerAndBone(hands[0].Fingers, "wskazujacy", 2);
            w3 = SelectFingerAndBone(hands[0].Fingers, "wskazujacy", 3);

            sr0 = SelectFingerAndBone(hands[0].Fingers, "srodkowy", 0);
            sr1 = SelectFingerAndBone(hands[0].Fingers, "srodkowy", 1);
            sr2 = SelectFingerAndBone(hands[0].Fingers, "srodkowy", 2);
            sr3 = SelectFingerAndBone(hands[0].Fingers, "srodkowy", 3);

            s0 = SelectFingerAndBone(hands[0].Fingers, "serdeczny", 0);
            s1 = SelectFingerAndBone(hands[0].Fingers, "serdeczny", 1);
            s2 = SelectFingerAndBone(hands[0].Fingers, "serdeczny", 2);
            s3 = SelectFingerAndBone(hands[0].Fingers, "serdeczny", 3);

            m0 = SelectFingerAndBone(hands[0].Fingers, "maly", 0);
            m1 = SelectFingerAndBone(hands[0].Fingers, "maly", 1);
            m2 = SelectFingerAndBone(hands[0].Fingers, "maly", 2);
            m3 = SelectFingerAndBone(hands[0].Fingers, "maly", 3);

            if (w2.y < k2.y && sr2.y < k2.y && s2.y < k2.y && m2.y < k2.y)
            {
                litera.text = "A";
            }
            else if (w3.y < sr3.y && sr3.y > s3.y && s3.y > m3.y && k3.x > w3.x)
            {
                litera.text = "B";
            }
            else if(m2.y > s3.y && m2.y > sr3.y && m2.y > w3.y)
            {
                litera.text = "M";
            }
            else if (w3.x < w2.x && k3.x < k2.x && w3.y > k3.y)
            {
                litera.text = "G";
            }
            else if (w3.y > k3.y && w3.y > sr3.y && w3.y > s3.y && w3.y > m3.y)
            {
                litera.text = "D";
            }
            else litera.text = " ";
        }
        else polaczenie.text = "Niepolaczony";
    }
}

