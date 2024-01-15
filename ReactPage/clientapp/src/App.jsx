import "./App.css";
import Header from "./components/Header";
import Footer from "./components/Footer";
import Links from "./components/Links";
import BodyText from "./components/BodyText";

function App() {
  return (
    <div className="App">
      <Header />
      <BodyText />
      <Links />
      <Footer />
    </div>
  );
}

export default App;
