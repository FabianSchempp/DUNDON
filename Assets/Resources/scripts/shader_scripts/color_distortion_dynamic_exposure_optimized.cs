using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]

public class color_distortion_dynamic_exposure_optimized: MonoBehaviour
{	
	public float multiplyer = 1;
	public float adjustment_speed = 5;
	public float exposure_mid_value = 0.5f;
	
	private RenderTexture[] sample_buffer = new RenderTexture[4];
	private int each_what_frame = 6;

	private int last_buffer_width = 2;
	private int last_buffer_height = 2;
	private Texture2D sample_buffer_texture;
	private float brightness_value = 0;
	private float exposure = 0.5f;
	private float exposure_target = 0.5f;
	private float tmp_value = 0;
	
	private Color[] colors;
	private int lambda = 0;
	private int iteration = 0;
	
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
		
		each_what_frame += sample_buffer.Length;
		
		//set exposure controll
		Shader.SetGlobalFloat("_exposure_control", 1);

	}
	
	void Update(){
		exposure = Mathf.Clamp(Mathf.Lerp(exposure, exposure_target, adjustment_speed * Time.deltaTime),0,2);
		Shader.SetGlobalFloat("_exposure_control", exposure);
//		Shader.SetGlobalFloat("_exposure_offset", exposure_offset);

	}

	// Called by the camera to apply the image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination) {
		if(Time.frameCount % each_what_frame == 0){
			//inizialize buffers
			if(sample_buffer[0] == null){
				//initializes sample_buffers
				for (int i = 0; i < sample_buffer.Length; i++){
					float exp = Mathf.Pow(4,i);
					sample_buffer[i] = new RenderTexture((int)(source.width/exp), (int)(source.height/exp), 0);
					sample_buffer[i].hideFlags = HideFlags.HideAndDontSave;
				}
				
				last_buffer_width = sample_buffer[sample_buffer.Length-1].width;
				last_buffer_height = sample_buffer[sample_buffer.Length-1].height;
			}
			
			//copy Buffers into each other and make each half the size;
			Graphics.Blit(source, sample_buffer[0]);
			
			//evaluate analysed_colors
			lambda = 0;
			tmp_value *= (1/(float)colors.Length);
			exposure_target = exposure_mid_value / (tmp_value * multiplyer);
			tmp_value = 0;
			
			Graphics.Blit(source, destination);
			iteration = 0;
		}
		else if (iteration < sample_buffer.Length-1){
			
			Graphics.Blit(sample_buffer[iteration], sample_buffer[iteration+1]);
			
			if (iteration == sample_buffer.Length-2){
				sample_buffer_texture = new Texture2D(last_buffer_width,last_buffer_height);
				sample_buffer_texture.ReadPixels(new Rect(0,0,last_buffer_width,last_buffer_height),0,0);
				colors = sample_buffer_texture.GetPixels();
			}
			
			Graphics.Blit(source, destination);
			iteration++;
		}
		else{
			analyse_colors();
			Graphics.Blit(source, destination);
		}	
	}
	
	void analyse_colors(){
		lambda ++;
		int i = (colors.Length/(each_what_frame - sample_buffer.Length)) * lambda;
		for (int x = 0; x< i; x++){
			//find approximate grayscale_value
			tmp_value += (colors[x].r*0.3f + colors[x].g*0.3f + colors[x].b*0.4f);
		}
	}
}	
