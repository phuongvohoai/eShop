import Image from "next/image";

const PageFooter = () => {
  return (
    <footer className=' bg-black w-full'>
      <div className='mx-auto max-w-screen-2xl'>
        <div className='py-14 px-40 text-white flex items-center justify-end sm:px-4 md:px-12'>
          <Image src="/logo-footer.svg" alt="Northern Mountains" width={102} height={88} quality={100} className="mr-auto" />
          <p>© Northern Mountains</p>
        </div>
      </div>
    </footer>
  );
}

export default PageFooter;
