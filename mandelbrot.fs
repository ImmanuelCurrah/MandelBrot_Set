#version 330 core
out vec4 FragColor;
uniform vec2 resolution;
uniform float time;
uniform vec2 uv;
uniform float m;

in vec3 ourColor;

vec2 hash12(float t) {
    float x = fract(sin(t * 3453.329));
    float y = fract(sin((t + x) * 8532.732));
    return vec2(x, y);
}

const float MAX_ITER = 128.0;

float mandelbrot(vec2 uv){
  vec2 c = 5.0 * uv - vec2(0.7, 0.0);
  vec2 z = vec2(0.0);
  float iter=0.0;

  c = c/pow(time,4.0) - vec2(0.65, 0.45);

  for(float i=0; i < MAX_ITER; i++){
    z = vec2(z.x * z.x - z.y * z.y, 
    2.0 * z.x * z.y) +c;
    if(dot(z,z) > 4.0) return iter/128.0;
    iter++;
  }
  return 0.0;
}

vec3 hash13(float m){
  float x = float(sin(m)*5625.246);
  float y = float(sin(m+x)*2216.486);
  float z = float(sin(x+y)*8276.351);
  return vec3(x,y,z);
}

void main()
{
  vec2 uv=((gl_FragCoord.xy-
  0.5*resolution.xy)/resolution.y);

  vec3 col = vec3(0.0);
  
  float m = mandelbrot(uv);
  col += hash13(m);
  FragColor = vec4(col, 1.0);
}