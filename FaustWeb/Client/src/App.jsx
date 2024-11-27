import './App.css'
import AccountActivity from "./components/personal_account_activity/activity.jsx";
import Account from "./components/personal_account_0/home.jsx";
import {Route, Routes} from "react-router-dom";

function App() {
  return (
    <>
        <Routes>
            <Route path='/' element={<Account />} />
            <Route path='/activity' element={<AccountActivity />} />
        </Routes>
    </>
  )
}

export default App;