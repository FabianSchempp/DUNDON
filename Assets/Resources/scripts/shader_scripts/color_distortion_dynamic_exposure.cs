using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]

public class color_distortion_dynamic_exposure: MonoBehaviour
{	
	public float multiplyer = 1;
	public int each_what_frame = 1;
	public float adjustment_speed = 5;

	private RenderTexture[] sample_buffer = new RenderTexture[4];
	private Texture2D sample_buffer_texture;
	private float brightness_value = 0;
	private float exposure = 0.5f;
	private float exposure_target = 0.5f;
	
	protected void OnEnable() {
		camera.depthTextureMode |= DepthTextureMode.Depth;		
	}
	
	protected void Start()
	{
		// Disable if we don't support image effects
		if (!SystemInfo.supportsImageEffects) {
			enabled = false;
			return;
		}	
	}
	
	void Update(){
		exposure = Mathf.Clamp(Mathf.Lerp(exposure, exposure_target, adjustment_speed * Time.deltaTime),0,2);
		Shader.SetGlobalFloat("_exposure_control", exposure);
	}

	// Called by the camera to apply the image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination) {
		if(Time.frameCount % each_what_frame == 0){
			if(sample_buffer[0] == null){
				//initializes sample_buffers
				for (int i = 0; i < sample_buffer.Length; i++){
					float exp = Mathf.Pow(4,i);
					sample_buffer[i] = new RenderTexture((int)(source.width/exp), (int)(source.height/exp), 0);
					sample_buffer[i].hideFlags = HideFlags.HideAndDontSave;
				}
			}
			//copy Buffers into each other and make each half the size;
			Graphics.Blit(source, sample_buffer[0]);
			for (int i = 0; i< sample_buffer.Length - 1; i++){
				Graphics.Blit(sample_buffer[i], sample_buffer[i+1]);
			}
	
//			Graphics.Blit(sample_buffer[sample_buffer.Length-1], destination);
			//output input
			Graphics.Blit(source, destination);

			int width = sample_buffer[sample_buffer.Length-1].width;
			int height = sample_buffer[sample_buffer.Length-1].height;
			sample_buffer_texture = new Texture2D(width, height);
			sample_buffer_texture.ReadPixels(new Rect(0,0,width,height),0,0);
			Color[] colors = sample_buffer_texture.GetPixels();
	
			for (int x = 0; x< colors.Length; x++){
	//			//find approximate grayscale_value
//				brightness_value += (colors[x].r*0.25f + colors[x].g*0.25f + colors[x].b*0.5f);
				brightness_value += (colors[x].r*0.3f + colors[x].g*0.3f + colors[x].b*0.4f);

			}
			brightness_value *= (1/(float)colors.Length);
			exposure_target = 0.5f / (brightness_value * multiplyer);
//			exposure_target = 1 - brightness_value * multiplyer;

		}
		else{
			Graphics.Blit(source, destination);
		}
		
	}
}	
