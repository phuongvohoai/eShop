/** @type {import('next').NextConfig} */
const nextConfig = {
  images: {
    remotePatterns: [
      {
        protocol: 'http',
        hostname: 'localhost',
        port: '50211',
        pathname: '/api/**',
      },
    ],
  },
};

export default nextConfig;
