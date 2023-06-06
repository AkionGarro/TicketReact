import { Carousel } from "@sefailyasoz/react-carousel";
import { FaArrowLeft, FaArrowRight } from "react-icons/fa";
import "../css/Carousel.css";
const ImagesSlider = () => {
  const CarouselData = [
    {
      subText: "Boxeo",
      image:
        "https://images.pexels.com/photos/598686/pexels-photo-598686.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
    },
    {
      subText: "Beisbol",
      image:
        "https://images.pexels.com/photos/269948/pexels-photo-269948.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
    },
    {
      subText: "Teatro",
      image:
        "https://images.pexels.com/photos/45258/ballet-production-performance-don-quixote-45258.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
    },
    {
      subText: "Futbol",
      image:
        "https://images.pexels.com/photos/1884574/pexels-photo-1884574.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
    },
    {
      subText: "Conciertos",
      image:
        "https://images.pexels.com/photos/167491/pexels-photo-167491.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
    },
    {
      subText: "Motocross",
      image:
        "https://images.pexels.com/photos/1448385/pexels-photo-1448385.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
    },
  ];

  return (
    <div className="containerCarousel">
      <Carousel
        data={CarouselData}
        autoPlay={true}
        rightItem={<FaArrowRight />}
        leftItem={<FaArrowLeft />}
        animationDuration={1000}
        headerTextType="black"
        subTextType="white"
        size="normal"
      />
    </div>
  );
};

export default ImagesSlider;
