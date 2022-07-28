#version 330 core
out vec4 FragColor;
uniform vec2 resolution;
uniform float time;
uniform vec2 uv;
uniform float m;

in vec3 ourColor;

vec2 rotate2D(vec2 uv, float a){
  float s =sin(a);
  float c = cos(a);
  return mat2(c, -s,s,c) * uv;
}

vec2 hash12(float t) {
    float x = fract(sin(t * 3453.329));
    float y = fract(sin((t + x) * 8532.732));
    return vec2(x, y);
}

// float mandelbrot(vec2 uv){
//   vec2 c = 5.0 * uv - vec2(0.7, 0.0);
//   vec2 z = vec2(0.0);
//   float iter=0.0;

//   for(float i; i <128.0; i++){
//     z = vec2(z.x * z.x - z.y * z.y, 
//     2.0 * z.x * z.y) +c;
//     if(dot(z,z) > 4.0) return iter/128.0;
//     iter++;
//   }
//   return 0.0;
// }

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
  
  uv = rotate2D(uv, 3.14 / 2.0);

  float r = 0.17;
  for (float i=0.0; i < 60.0; i++) {
      float factor = (sin(time) * 0.5 + 0.5) + 0.3;
      i += factor;

      float a = i / 3;
      float dx = 2 * r * cos(a) - r * cos(2 * a);
      float dy = 2 * r * sin(a) - r * sin(2 * a);

      col += 0.013 * factor / length(uv - vec2(dx + 0.1, dy) - 0.02 * hash12(i));
  }
  col *= sin(vec3(0.2, 0.8, 0.9) * time) * 0.15 + 0.25;
  FragColor = vec4(col, 1.0);
}
