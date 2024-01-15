import React from "react";

const Links = () => {
  const navigateToProductPage = () => {
    window.location.href = "/Product/Index"; // Adjust the URL as needed
  };

  return (
    <div>
      <h2>
        <button className="App-link" onClick={navigateToProductPage}>
          SEE PRODUCT PAGES
        </button>
      </h2>
    </div>
  );
};

export default Links;
