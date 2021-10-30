<template>
  <div>
    <article class="article" v-for="(article, index) in articles" :key="index">
      <div class="article-image">
        <img :src="article.image" alt="Article">
      </div>
      <div class="article-details">
        <h3>{{article.title}}</h3>
        <div class="article-description">
          <p>{{article.content}}</p>
        </div>
        <ul>
          <li>Fuente: {{article.source.name}}</li>
          <li>Publicado el: {{article.publishedAt}}</li>
        </ul>
        <div class="article-link">
          <b-button variant="primary" :href="article.url">Ver Contenido</b-button>
        </div>
      </div>
    </article>
  </div>
</template>
<script>
import axios from "axios";
import blogType from "@/utils/blogType";
import { mapMutations } from "vuex";
export default {
  name: "Remote",
  data() {
    return {
      articles: []
    }
  },
  beforeMount() {
    this.getInitialArticles();
  },
  mounted() {
    this.$store.commit("blog/SET_BLOG_TYPE", blogType.REMOTE);
    this.getArticles();
  },
  methods: {
    ...mapMutations(["blog/SET_BLOG_TYPE"]),
    getInitialArticles() {
      const apitoken = process.env.VUE_APP_API_TOKEN;
      const keyword = encodeURIComponent("on sale");
      const uri = `https://gnews.io/api/v4/search?q=${keyword}&token=${apitoken}`;
      axios.get(uri).then(response => {
        this.articles = response.data.articles;
      });
    },
    getArticles() {
      window.onscroll = () => {
        let bottomOfWindow = document.documentElement.scrollTop + window.innerHeight === document.documentElement.offsetHeight;
        if (bottomOfWindow) {
          const apitoken = process.env.VUE_APP_API_TOKEN;
          const keyword = encodeURIComponent("on sale");
          const uri = `https://gnews.io/api/v4/search?q=${keyword}&token=${apitoken}`;
          axios.get(uri).then(response => {
            this.articles.concat(response.data.articles);
          });
        }
      }
    }
  },
};
</script>
<style>
  .article {
    display: flex;
    font-family: "Trirong", sans-serif;
    background: #2D326C;
    border-radius: 1em;
    margin: 1.5em auto;
    color: white;
  }
  .article-image {
    padding: 1em;
  }
  .article-image img {
    width: 250px;
    height: 250px;
    border-radius: 1em;
  }
  .article-details {
    padding: 1em;
  }
  .article-details h3 {
    text-align: start;
  }
  .article-details ul {
    padding: 0;
    margin: 0;
    text-align: left;
  }
  .article-details li {
    list-style: none;  
  }
  .article-description {
    text-align: justify;
  }
  .article-link {
    text-align: left;
    margin-top: 1em;
  }
</style>