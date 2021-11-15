using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    public Transform cameraTransform;
    public GameObject cameraPreviewObject;
    public Transform actorTransform;
    public GameObject actorObject;
    public GameObject screen1;
    public GameObject screen2;
    public RawImage[] screenplayRawImage;
    
    //Camera control keys
    public KeyCode cameraMoveLeft = KeyCode.A;
	public KeyCode cameraMoveRight = KeyCode.D;
	public KeyCode cameraMoveUp = KeyCode.W;
	public KeyCode cameraMoveDown = KeyCode.S;
    public KeyCode cameraZoomIn = KeyCode.Q;
    public KeyCode cameraZoomOut = KeyCode.E;

    //Actor control keys
    public KeyCode actorMoveLeft = KeyCode.J;
	public KeyCode actorMoveRight = KeyCode.L;
	public KeyCode actorMoveUp = KeyCode.I;
	public KeyCode actorMoveDown = KeyCode.K;
    public KeyCode actorZoomIn = KeyCode.U;
    public KeyCode actorZoomOut = KeyCode.O;

    //Data controls
    public KeyCode setDefault = KeyCode.R;
    public KeyCode saveData = KeyCode.Tab;
    public KeyCode loadData = KeyCode.BackQuote;
    public KeyCode showCamera = KeyCode.Backspace;

    float posX, posY, posZ;
    float scalePosX, scalePosY, scalePosZ; 

    float actorPosX, actorPosY, actorPosZ; 
    float actorScalePosX, actorScalePosY, actorScalePosZ;
    public float actR, actG, actB;
    Vector3 cameraPosition, cameraScale, actorScale, actorPosition;
    Color actorColor, cameraColor;
    Graphic actorGraphic, cameraGraphic;
    // 0 - Camera, 1 - Arrow, 2 - default
    int showCam = 0;

    // Start is called before the first frame update
    void Start()
    {
        //actorColorPicker.SetActive(false);
        //cameraColorPicker.SetActive(false);
        actorObject.SetActive(false);
        Cursor.visible = false;
        //actorGraphic = actorObject.GetComponent<Graphic>();
        //cameraGraphic = cameraPreviewObject.GetComponent<Graphic>();

        // Initialise the colors for actor
        actorColor = new Color(255f, 255f, 255f, 255f);
        cameraColor = new Color(255f, 255f, 255f, 255f);

        //Get saved camera positions from player prefs
        posX = PlayerPrefs.HasKey("posX") ?
            PlayerPrefs.GetFloat("posX") : 0.0F;
        posY = PlayerPrefs.HasKey("posY") ?
            PlayerPrefs.GetFloat("posY") : 0.0F;
        posZ = PlayerPrefs.HasKey("posZ") ?
            PlayerPrefs.GetFloat("posZ") : 0.0F;

        scalePosX = PlayerPrefs.HasKey("scalePosX") ?
            PlayerPrefs.GetFloat("scalePosX") : -1.0F;
        scalePosY = PlayerPrefs.HasKey("ScalePosY") ?
            PlayerPrefs.GetFloat("ScalePosY") : 1.0F;
        scalePosZ = PlayerPrefs.HasKey("ScalePosZ") ?
            PlayerPrefs.GetFloat("ScalePosZ") : 1.0F;

        //Get saved actor's position from player prefs
        actorPosX = PlayerPrefs.HasKey("actorPosX") ?
            PlayerPrefs.GetFloat("actorPosX") : 0.0F;
        actorPosY = PlayerPrefs.HasKey("actorPosY") ?
            PlayerPrefs.GetFloat("actorPosY") : 0.0F;
        actorPosZ = PlayerPrefs.HasKey("actorPosZ") ?
            PlayerPrefs.GetFloat("actorPosZ") : 0.0F;

        actorScalePosX = PlayerPrefs.HasKey("actorScalePosX") ?
            PlayerPrefs.GetFloat("actorScalePosX") : 1.0F;
        actorScalePosY = PlayerPrefs.HasKey("actorScalePosY") ?
            PlayerPrefs.GetFloat("actorScalePosY") : 1.0F;
        actorScalePosZ = PlayerPrefs.HasKey("actorScalePosZ") ?
            PlayerPrefs.GetFloat("actorScalePosZ") : 1.0F;

        actorColor[0] = PlayerPrefs.HasKey("actR") ?
            PlayerPrefs.GetFloat("actR") : 255.0F;
        actorColor[1] = PlayerPrefs.HasKey("actG") ?
            PlayerPrefs.GetFloat("actG") : 255.0F;
        actorColor[2] = PlayerPrefs.HasKey("actB") ?
            PlayerPrefs.GetFloat("actB") : 255.0F;

        cameraColor[0] = PlayerPrefs.HasKey("camR") ?
            PlayerPrefs.GetFloat("camR") : 255.0F;
        cameraColor[1] = PlayerPrefs.HasKey("camG") ?
            PlayerPrefs.GetFloat("camG") : 255.0F;
        cameraColor[2] = PlayerPrefs.HasKey("camB") ?
            PlayerPrefs.GetFloat("camB") : 255.0F;

        // Initialise the vectors for CameraPreview
        cameraPosition = new Vector3(posX, posY, posZ);
        cameraScale = new Vector3(scalePosX, scalePosY, scalePosZ);

        // Initialise the vectors for screenplay actor
        actorPosition = new Vector3(actorPosX, actorPosY, actorPosZ);
        actorScale = new Vector3(actorScalePosX, actorScalePosY, actorScalePosZ);

        cameraPreviewObject.transform.localScale = cameraScale;
        cameraTransform.position = cameraPosition;

        actorObject.transform.localScale = actorScale;
        actorTransform.position = actorPosition;
        DataLoad();
    }

    // Update is called once per frame
    void Update()
    {
        //actorColor = actorGraphic.color;
        //cameraColor = cameraGraphic.color;

        if (Input.GetKeyUp(showCamera)) {
            if (showCam == 0) {
                screen1.SetActive(false);
                screen2.SetActive(false);
                //actorColorPicker.SetActive(true);
                //cameraColorPicker.SetActive(true);
                actorObject.SetActive(true);
                screenplayRawImage[0].enabled = true;
                Cursor.visible = true;
                GameObject.FindObjectOfType<VideoController>().OpenVideo();
                GameObject.FindObjectOfType<StandbyScreenplaySelector>().CoroutineStarter(false);
                showCam = 1;
            } //else if (showCam == 1) {
                //screen2.SetActive(true);
                //showCam = 2;
            //}
            else {
                screen1.SetActive(true);
                screen2.SetActive(false);
                //actorColorPicker.SetActive(false);
                //cameraColorPicker.SetActive(false);
                GameObject.FindObjectOfType<VideoController>().CloseVideo();
                actorObject.SetActive(false);
                //screenplayRawImage[0].enabled = false;
                Cursor.visible = false;
                showCam = 0;
            }
            Debug.Log("show cam " + showCam);
        }

        //Camera controls ------------------------------------------------------------------
        //Camera controls ------------------------------------------------------------------
        //Camera controls ------------------------------------------------------------------

        if (Input.GetKeyUp(cameraZoomIn)) {
            cameraScale.x -= 0.01F;
            cameraScale.y += 0.01F;
            cameraScale.z += 0.01F;

            cameraPreviewObject.transform.localScale = cameraScale;

            Debug.Log("Key " + cameraZoomIn + " released " + cameraScale);
        }

        if (Input.GetKeyUp(cameraZoomOut)) {
            cameraScale.x += 0.01F;
            cameraScale.y -= 0.01F;
            cameraScale.z -= 0.01F;

            cameraPreviewObject.transform.localScale = cameraScale;

            Debug.Log("Key " + cameraZoomOut + " released " + cameraScale);
        }

        if (Input.GetKeyUp(cameraMoveLeft)) {
            cameraPosition.x -= 0.02F;
            cameraTransform.position = cameraPosition;

            Debug.Log("Key " + cameraMoveLeft + " released " + cameraPosition);
        }

        if (Input.GetKeyUp(cameraMoveRight)) {
            cameraPosition.x += 0.02F;
            cameraTransform.position = cameraPosition;

            Debug.Log("Key " + cameraMoveRight + " released " + cameraPosition);
        }

        if (Input.GetKeyUp(cameraMoveDown)) {
            cameraPosition.y -= 0.01F;
            cameraTransform.position = cameraPosition;

            Debug.Log("Key " + cameraMoveDown + " released " + cameraPosition);
        }

        if (Input.GetKeyUp(cameraMoveUp)) {
            cameraPosition.y += 0.01F;
            cameraTransform.position = cameraPosition;
            
            Debug.Log("Key " + cameraMoveUp + " released " + cameraPosition);
        }

        //Actor controls ------------------------------------------------------------------
        //Actor controls ------------------------------------------------------------------
        //Actor controls ------------------------------------------------------------------

        if (Input.GetKeyUp(actorZoomIn)) {
            actorScale.x += 0.01F;
            actorScale.y += 0.01F;
            actorScale.z += 0.01F;

            actorObject.transform.localScale = actorScale;

            Debug.Log("Key " + actorZoomIn + " released " + cameraScale);
        }

        if (Input.GetKeyUp(actorZoomOut)) {
            actorScale.x -= 0.01F;
            actorScale.y -= 0.01F;
            actorScale.z -= 0.01F;

            actorObject.transform.localScale = actorScale;

            Debug.Log("Key " + actorZoomOut + " released " + actorScale);
        }

        if (Input.GetKeyUp(actorMoveLeft)) {
            actorPosition.x -= 0.02F;
            actorTransform.position = actorPosition;

            Debug.Log("Key " + actorMoveLeft + " released " + actorPosition);
        }

        if (Input.GetKeyUp(actorMoveRight)) {
            actorPosition.x += 0.02F;
            actorTransform.position = actorPosition;

            Debug.Log("Key " + actorMoveRight + " released " + actorPosition);
        }

        if (Input.GetKeyUp(actorMoveDown)) {
            actorPosition.y -= 0.01F;
            actorTransform.position = actorPosition;

            Debug.Log("Key " + actorMoveDown + " released " + actorPosition);
        }

        if (Input.GetKeyUp(actorMoveUp)) {
            actorPosition.y += 0.01F;
            actorTransform.position = actorPosition;
            
            Debug.Log("Key " + actorMoveUp + " released " + actorPosition);
        }

        //Data controls ------------------------------------------------------------------
        //Data controls ------------------------------------------------------------------
        //Data controls ------------------------------------------------------------------

        if (Input.GetKeyUp(setDefault)){
            cameraPosition.x = 0.0F;
            cameraPosition.y = 0.0F;
            cameraPosition.z = 0.0F;

            cameraScale.x = -1.0F;
            cameraScale.y = 1.0F;
            cameraScale.z = 1.0F;

            actorPosition.x = 0.0F;
            actorPosition.y = 0.0F;
            actorPosition.z = 0.0F;

            actorScale.x = 1.0F;
            actorScale.y = 1.0F;
            actorScale.z = 1.0F;

            cameraPreviewObject.transform.localScale = cameraScale;
            cameraTransform.position = cameraPosition;

            actorObject.transform.localScale = actorScale;
            actorTransform.position = actorPosition;

            Debug.Log("Key " + setDefault + " released " + cameraPosition + " " + cameraScale);
        }

        if (Input.GetKeyUp(saveData)){
            //camera prefs
            PlayerPrefs.SetFloat("posX", cameraPosition.x);
            PlayerPrefs.SetFloat("posY", cameraPosition.y);
            PlayerPrefs.SetFloat("posZ", cameraPosition.z);

            PlayerPrefs.SetFloat("scalePosX", cameraScale.x);
            PlayerPrefs.SetFloat("scalePosY", cameraScale.y);
            PlayerPrefs.SetFloat("scalePosZ", cameraScale.z);
            
            //actor prefs
            PlayerPrefs.SetFloat("actorPosX", actorPosition.x);
            PlayerPrefs.SetFloat("actorPosY", actorPosition.y);
            PlayerPrefs.SetFloat("actorPosZ", actorPosition.z);

            PlayerPrefs.SetFloat("actorScalePosX", actorScale.x);
            PlayerPrefs.SetFloat("actorScalePosY", actorScale.y);
            PlayerPrefs.SetFloat("actorScalePosZ", actorScale.z);

            PlayerPrefs.SetFloat("actR", actorColor[0]);
            PlayerPrefs.SetFloat("actG", actorColor[1]);
            PlayerPrefs.SetFloat("actB", actorColor[2]);

            PlayerPrefs.SetFloat("camR", cameraColor[0]);
            PlayerPrefs.SetFloat("camG", cameraColor[1]);
            PlayerPrefs.SetFloat("camB", cameraColor[2]);

            PlayerPrefs.Save();

            Debug.Log("Data saved to player prefs");
            Debug.Log("actorPosX " + PlayerPrefs.GetFloat("actorPosX"));
            Debug.Log("actorPosY " + PlayerPrefs.GetFloat("actorPosY"));
            Debug.Log("actorPosZ " + PlayerPrefs.GetFloat("actorPosZ"));
            Debug.Log("actorScalePosX " + PlayerPrefs.GetFloat("actorScalePosX"));
            Debug.Log("actorScalePosY " + PlayerPrefs.GetFloat("actorScalePosY"));
            Debug.Log("actorScalePosZ " + PlayerPrefs.GetFloat("actorScalePosZ"));
            Debug.Log($"R = {actorColor[0]}, G = {actorColor[1]}, B = {actorColor[2]}, ");
            Debug.Log($"PPActR = {PlayerPrefs.GetFloat("actR", actorColor[0])}, PPActG = {PlayerPrefs.GetFloat("actG", actorColor[1])}, PPActB = {PlayerPrefs.GetFloat("actB", actorColor[2])},");
            

        }

        if (Input.GetKeyUp(loadData)){
        DataLoad();
        }
    }
    public void DataLoad (){
            cameraPosition.x = PlayerPrefs.GetFloat("posX");
            cameraPosition.y = PlayerPrefs.GetFloat("posY");
            cameraPosition.z = PlayerPrefs.GetFloat("posZ");

            cameraScale.x = PlayerPrefs.GetFloat("scalePosX");
            cameraScale.y = PlayerPrefs.GetFloat("scalePosY");
            cameraScale.z = PlayerPrefs.GetFloat("scalePosZ");


            actorPosition.x = PlayerPrefs.GetFloat("actorPosX");
            actorPosition.y = PlayerPrefs.GetFloat("actorPosY");
            actorPosition.z = PlayerPrefs.GetFloat("actorPosZ");

            actorScale.x = PlayerPrefs.GetFloat("actorScalePosX");
            actorScale.y = PlayerPrefs.GetFloat("actorScalePosY");
            actorScale.z = PlayerPrefs.GetFloat("actorScalePosZ");
            
            actorColor[0] = PlayerPrefs.GetFloat("actR");
            actorColor[1] = PlayerPrefs.GetFloat("actG");
            actorColor[2] = PlayerPrefs.GetFloat("actB");

            cameraColor[0] = PlayerPrefs.GetFloat("camR");
            cameraColor[1] = PlayerPrefs.GetFloat("camG");
            cameraColor[2] = PlayerPrefs.GetFloat("camB");

            //actorGraphic.color = actorColor;
            //cameraGraphic.color = cameraColor;

            cameraPreviewObject.transform.localScale = cameraScale;
            cameraTransform.position = cameraPosition;

            actorObject.transform.localScale = actorScale;
            actorTransform.position = actorPosition;
        }
}
