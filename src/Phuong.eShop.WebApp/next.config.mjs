/** @type {import('next').NextConfig} */
const nextConfig = {
  images: {
    remotePatterns: [
      {
        protocol: 'http',
        hostname: 'localhost',
        port: '12270',
        pathname: '/api/files/**',
      },
    ],
  },
};

export default nextConfig;
