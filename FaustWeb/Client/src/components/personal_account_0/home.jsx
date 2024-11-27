import './home.css'
import {useState} from "react";
import {useLocation, useNavigate} from "react-router-dom";

function Account() {
    const [isOpen, setIsOpen] = useState(false)
    const data = true;
    const location = useLocation();
    const isHomeActive = location.pathname === '/';
    const navigate = useNavigate();

    function toggleMenu() {
        setIsOpen(!isOpen)
    }
    return (
        <div className="main">
            <div className="header">
                <div className="header-left">
                    <button id="menu-icon"><img src="/menu.svg"/></button>
                    <img id="faust-icon" src="/faust.png"/>
                    <span className="site-title">FAUST · ФАУСТ</span>
                </div>
                <div className="header-right">
                    <button id="search-icon"><img src="/search.svg"/></button>
                    <button onClick={toggleMenu} id="bell-icon"><img src="/bell.svg"/></button>
                    {isOpen && (
                        <div className="messages">
                            <div className="messages-header">
                                <div className="messages-title">Сповіщення</div>
                                <button className="messages-selector">Новинки <img src="/chevron-down%20(1).svg"/>
                                </button>
                            </div>
                            <div className="messages-content">
                                <div className="blockMessage" id="firstMessage"><img src="/heart%20(1).svg"/>Ви
                                    вподобали мангу “Уперед, Накамура-кун!!!”
                                </div>
                                <div className="blockMessage" id="secondMessage"><img src="/alert-triangle.svg"/>Вийшло
                                    оновлення сайту 4.4.1, Деталі тут
                                </div>
                                <div className="blockMessage" id="thirdMessage"><img src="/message-square.svg"/>Користувач
                                    DeathGod відповів на ваш коментар до проєкту “Зошит смерті”
                                </div>
                                <div className="blockMessage" id="fourthMessage"><img src="/bookmark%20(1).svg"/>Ви
                                    зберегли тайтл “Компанія та особисте життя” до закладинок “Читаю”
                                </div>
                            </div>
                        </div>
                    )}
                    <div id="message-count">4</div>
                </div>
            </div>
            <div className="profile-banner">
                <img src="/banner.png"/>
            </div>
            <div className="profile-logo"><img id="profile-logo" src="/profile.jpg"/></div>
            <div className="class">
                <div className="profile-ids">
                    <div className="profile-name">Naruto_Uzumaki123</div>
                    <div className="profile-username">@konoha_1</div>
                </div>
                <div className="action-buttons">
                    <button id="edit-profile"><img src="/edit-3.svg"/>Редагувати профіль</button>
                    <button id="create-team"><img src="/user.svg"/>Створити команду</button>
                </div>
            </div>
            <div className="tabs-buttons">
                <button onClick={() => navigate('/activity')} id="activity"><img src="/trending-up.svg"/>Активність</button>
                <button style={{ backgroundColor: isHomeActive ? '#59357D' : '#392854' }} id="bookmarks"><img src="/bookmark.svg"/>Закладинки</button>
                <button id="likes"><img src="/heart.svg"/>Вподобайки</button>
                <button id="comments"><img src="/message-circle.svg"/>Коментарі</button>
            </div>
            <div className="books-buttons">
                <button id="add-bookmark">Додати закладку<img src="/plus.svg"></img></button>
                <button id="reading">Читаю (10)<img src="/chevron-down.svg"></img></button>
            </div>
            {data ? (
    <div className="main-content" style={{justifyContent: 'flex-start', paddingLeft: '8px',paddingBottom: '16px', paddingRight: '8px', paddingTop: '8px', gap: '24px', height: 'auto'}}>
        <div className="book" id="book1">
            <div className="book-date">04.04.2024 09:17</div>
            <div className="signatures">
                <div className="status">ПЕРЕКЛАДАЄТЬСЯ</div>
                <div className="type">МАНХВА</div>
            </div>
            <img className="book-img" src="/book1.svg"></img>
            <div className="book-title">Дні Сакамото</div>
        </div>
        <div className="book" id="book2">
            <div className="book-date">10.09.2024 17:24</div>
            <div className="signatures">
                <div className="status">ПЕРЕКЛАДАЄТЬСЯ</div>
                <div className="type">МАНХВА</div>
            </div>
            <img className="book-img" src="/book2.svg"></img>
            <div className="book-title">Від кохання до...</div>
        </div>
        <div className="book" id="book3">
            <div className="book-date">01.02.2024 01:31</div>
            <div className="signatures">
                <div className="status">ПЕРЕКЛАДАЄТЬСЯ</div>
                <div className="type">МАНХВА</div>
            </div>
            <img className="book-img" src="/book3.svg"></img>
            <div className="book-title">Милий дім</div>
        </div>
    </div>
) : (
    <div className="main-content" style={{alignItems: 'center', justifyContent: 'center', height:'330px'}}>
        Немає збережених...
    </div>
)}
        </div>
    )
}

export default Account